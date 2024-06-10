using Microsoft.ML.Data;

namespace AnomalyDetection.Models
{
    public class ToxicPrediction
    {
        [ColumnName("PredictedLabel")]
        public string? PhraseType;
    }
}