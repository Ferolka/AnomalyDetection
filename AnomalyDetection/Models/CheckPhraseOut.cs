using Common.Constantans;

namespace AnomalyDetection.Models
{
    public record CheckPhraseOut(bool IsToxic, long PredictedPhraseId);
}