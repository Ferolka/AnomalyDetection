using AnomalyDetection.Interfaces;
using AnomalyDetection.Models;
using AnomalyDetection.Options;
using AnomalyDetection.UseCases;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.ML;

namespace AnomalyDetection.Extensions
{
    public static class AnomalyDetectionServiceCollectionExtensions
    {
        public static IServiceCollection AddAnomalyDetectionServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            // Register the PredictionEnginePool as a service in the IoC container for DI
            //
            services.AddPredictionEnginePool<PhraseML, ToxicPrediction>()
                .FromFile(configuration[
                    $"{TrainedDataOptions.TrainedDataOptionsSection}:" +
                    $"{nameof(TrainedDataOptions.TrainedModelPath)}"]);

            services.Configure<TrainedDataOptions>(configuration
                .GetSection(TrainedDataOptions.TrainedDataOptionsSection));

            services.AddScoped<ITrainUseCase, TrainUseCase>();
            services.AddScoped<ICheckPhraseUseCase, CheckPhraseUseCase>();
            services.AddScoped<IPredictedPhraseUseCase, PredictedPhraseUseCase>();

            return services;
        }
    }
}