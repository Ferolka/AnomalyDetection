﻿using AnomalyDetection.Interfaces;
using AnomalyDetection.Models;
using Common.Entities;
using Common.Interfaces;
using Common.Models;
using Microsoft.Extensions.ML;

namespace AnomalyDetection.UseCases
{
    internal class CheckPhraseUseCase : ICheckPhraseUseCase
    {
        private readonly PredictionEnginePool<PhraseML, ToxicPrediction> _predictionEnginePool;
        private readonly IPredictedPhraseStore _predictedPhraseStore;

        public CheckPhraseUseCase(
            PredictionEnginePool<PhraseML, ToxicPrediction> predictionEnginePool,
            IPredictedPhraseStore predictedPhraseStore)
        {
            this._predictionEnginePool = predictionEnginePool
                ?? throw new ArgumentNullException(nameof(predictionEnginePool));
            this._predictedPhraseStore = predictedPhraseStore
                ?? throw new ArgumentNullException(nameof(predictedPhraseStore));
        }

        public async Task<ResultModel<CheckPhraseOut>> ExecuteAsync(PhraseML phraseML, CancellationToken cancellationToken)
        {
            // Step 1: Make prediction
            var prediction = this._predictionEnginePool.Predict(phraseML);
            var isToxic = prediction.Prediction;

            // Step 2: Save prediction
            var addPredictedPhrase = await this.AddPredictedPhrase(
                phraseML,
                isToxic,
                cancellationToken);

            if (!addPredictedPhrase.IsSuccess)
            {
                return ResultModel<CheckPhraseOut>.FromUnexpectedError(addPredictedPhrase.Exception!);
            }

            return ResultModel<CheckPhraseOut>.FromSuccess(new CheckPhraseOut(
                isToxic,
                addPredictedPhrase.Entity!));
        }

        private async Task<ResultModel<long>> AddPredictedPhrase(
            PhraseML phraseML,
            bool isToxic,
            CancellationToken cancellationToken)
        {
            var predictedPhrase = new PredictedPhrase
            {
                Checked = false,
                IsToxic = isToxic,
                Text = phraseML.Text!,
            };
            return await this._predictedPhraseStore
                .AddPredictedPhraseIfNotExistsAsync(predictedPhrase, cancellationToken);
        }
    }
}