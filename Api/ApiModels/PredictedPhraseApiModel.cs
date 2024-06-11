using Common.Constantans;
using System.Text.Json.Serialization;

namespace Api.ApiModels
{
    public class PredictedPhraseApiModel
    {
        public long Id { get; set; }
        public string Text { get; set; } = string.Empty;
        public bool IsToxic { get; set; }
        public bool Checked { get; set; }
        public string? Comment { get; set; }
    }
}
