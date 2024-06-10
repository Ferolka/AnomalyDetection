using System.Text.Json.Serialization;

namespace Api.ApiModels
{
    public record CheckPhraseApiModel(string Phrase)
    {
        [JsonPropertyName("phrase")]
        public string Phrase { get; set; } = Phrase;
    }
}
