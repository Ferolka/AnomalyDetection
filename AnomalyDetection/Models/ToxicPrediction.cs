using Microsoft.ML.Data;

namespace AnomalyDetection.Models
{
    public class ToxicPrediction
    {
        [ColumnName("PredictedLabel")]
        public bool Prediction { get; set; }

        public float Probability { get; set; }

        public float Score { get; set; }
    }
}