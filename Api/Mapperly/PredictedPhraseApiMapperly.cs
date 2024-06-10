using AnomalyDetection.Models;
using Api.ApiModels;
using Common.Entities;
using Riok.Mapperly.Abstractions;

namespace Api.Mapperly
{
    [Mapper]
    internal static partial class PredictedPhraseApiMapperly
    {
        internal static partial PredictedPhrase Map(PredictedPhraseApiModel source);

        internal static partial PredictedPhraseApiModel Map(PredictedPhrase source);
    }
}