using Common.Constantans;
using System.Text.Json.Serialization;

namespace Api.ApiModels
{
    public record CheckPhraseResponseApiModel(bool IsToxic, long PredictedPhraseId);
}
