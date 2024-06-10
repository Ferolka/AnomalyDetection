using Common.Constantans;
using System.Text.Json.Serialization;

namespace Api.ApiModels
{
    public record CheckPhraseResponseApiModel(PhraseType PhraseType, long PredictedPhraseId)
    {
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public PhraseType PhraseType { get; init; } = PhraseType;
    }
}
