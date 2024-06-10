using AnomalyDetection.Interfaces;
using AnomalyDetection.Mapperly;
using AnomalyDetection.Models;
using Common.Entities;
using Common.Interfaces;
using Common.Models;

namespace AnomalyDetection.UseCases
{
    internal class PredictedPhraseUseCase : IPredictedPhraseUseCase
    {
        private readonly IPredictedPhraseStore _predictedPhraseStore;
        private readonly IUnitOfWorkStore _unitOfWorkStore;

        public PredictedPhraseUseCase(
            IPredictedPhraseStore predictedPhraseStore,
            IUnitOfWorkStore unitOfWorkStore)
        {
            this._predictedPhraseStore = predictedPhraseStore
                ?? throw new ArgumentNullException(nameof(predictedPhraseStore));
            this._unitOfWorkStore = unitOfWorkStore
                ?? throw new ArgumentNullException(nameof(unitOfWorkStore));
        }

        public async Task<ResultModel> VerifyPredictedPhrases(
            List<PredictedPhrase> predictedPhrases,
            CancellationToken cancellationToken)
        {
            var phrasesForTrain = predictedPhrases.ConvertAll(PredictedPhraseMapperly.Map);
            this._unitOfWorkStore.UpdateRange(predictedPhrases);
            this._unitOfWorkStore.AddRange(phrasesForTrain);

            var result = await this._unitOfWorkStore.SaveChangesAsync(cancellationToken);

            return result.IsSuccess
                ? ResultModel.FromSuccess()
                : result;
        }

        public async Task<ResultModel<List<PredictedPhrase>>> GetUncheckedPredictedPhrases(
            PaginatedModel paginatedModel,
            CancellationToken cancellationToken)
        {
            var top = paginatedModel.PageSize;
            var skip = paginatedModel.PageSize * (paginatedModel.PageNumber - 1);
            return await this._predictedPhraseStore.GetUncheckedPredictedPhrasesAsync(
                top,
                skip,
                cancellationToken);
        }
    }
}