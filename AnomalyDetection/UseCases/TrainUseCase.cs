using AnomalyDetection.Interfaces;
using AnomalyDetection.Mapperly;
using AnomalyDetection.Options;
using Common.Entities;
using Common.Interfaces;
using Common.Models;
using Microsoft.Extensions.Options;
using Microsoft.ML;
using Microsoft.ML.Trainers;

namespace AnomalyDetection.UseCases
{
    internal class TrainUseCase : ITrainUseCase
    {
        private readonly IPhraseStore _toxicWordStore;
        private readonly TrainedDataOptions _trainedDataOptions;

        public TrainUseCase(
            IPhraseStore toxicWordStore,
            IOptions<TrainedDataOptions> trainedDataOptions)
        {
            this._toxicWordStore = toxicWordStore
                ?? throw new ArgumentNullException(nameof(toxicWordStore));
            this._trainedDataOptions = trainedDataOptions?.Value
                ?? throw new ArgumentNullException(nameof(trainedDataOptions));
        }

        public async Task<ResultModel> ExecuteAsync(CancellationToken cancellationToken)
        {
            // Step 1: Get "all" available data from Db.
            var getToxicWordsResult = await this.GetImageDataAsync(cancellationToken);
            if (!getToxicWordsResult.IsSuccess)
            {
                return ResultModel.FromDbError(getToxicWordsResult.Exception!);
            }

            var productSalesDataList = getToxicWordsResult.Entity!;

            // Step 2 Retrain
            Retrain(productSalesDataList);

            return ResultModel.FromSuccess();
        }

        private async Task<ResultModel<List<Phrase>>> GetImageDataAsync(CancellationToken cancellationToken)
        {
            return await this._toxicWordStore
                .GetPhrasesAsync(cancellationToken);
        }

        private void Retrain(List<Phrase> toxicWordList)
        {
            // Create MLContext
            var mlContext = new MLContext();

            var toxicWordMlList = toxicWordList.ConvertAll(PhraseMLMapperly.Map);

            //Load New Data
            IDataView toxicWordDataSet = mlContext.Data.LoadFromEnumerable(toxicWordMlList);

            var options = new SdcaNonCalibratedBinaryTrainer.Options()
            {
                // Specify loss function.

                // Make the convergence tolerance tighter.
                ConvergenceTolerance = 0.05f,
                // Increase the maximum number of passes over training data.
                MaximumNumberOfIterations = 50,
                // Give the instances of the positive class slightly more weight.
                LabelColumnName = "IsToxic", // Use the Boolean column directly
                FeatureColumnName = "Features"
            };

            var preprocessingPipeline = mlContext.Transforms
                .Text.FeaturizeText(inputColumnName: "Text", outputColumnName: "Features")
                .Append(mlContext.BinaryClassification.Trainers.SdcaNonCalibrated(options));

            var transformedModel = preprocessingPipeline.Fit(toxicWordDataSet);

            SaveModel(mlContext, transformedModel, this._trainedDataOptions.TrainedModelPath, toxicWordDataSet);
        }

        private static void SaveModel(MLContext mlContext, ITransformer trainedModel, string modelPath, IDataView dataView)
        {
            mlContext.Model.Save(trainedModel, dataView.Schema, modelPath);
        }
    }
}