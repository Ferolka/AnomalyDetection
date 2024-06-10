using Common.Constantans;

namespace AnomalyDetection.Models
{
    public record CheckPhraseOut(PhraseType PhraseType, long PredictedPhraseId);
}