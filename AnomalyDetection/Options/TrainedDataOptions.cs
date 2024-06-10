using System.ComponentModel.DataAnnotations;

namespace AnomalyDetection.Options
{
    public class TrainedDataOptions
    {
        public const string TrainedDataOptionsSection = "TrainedData";

        [Required]
        public string TrainedModelPath { get; set; } = string.Empty;
    }
}