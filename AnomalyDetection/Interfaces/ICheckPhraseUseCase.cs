using AnomalyDetection.Models;
using Common.Models;

namespace AnomalyDetection.Interfaces
{
    public interface ICheckPhraseUseCase
    {
        Task<ResultModel<CheckPhraseOut>> ExecuteAsync(PhraseML phraseML, CancellationToken cancellationToken);
    }
}