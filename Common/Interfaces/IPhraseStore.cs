using Common.Entities;
using Common.Models;

namespace Common.Interfaces
{
    public interface IPhraseStore
    {
        Task<ResultModel<List<Phrase>>> GetPhrasesAsync(
            CancellationToken cancellationToken, int limit = 1000);

        Task<ResultModel<List<long>>> AddPhrasesAsync(List<Phrase> toxicWords,
            CancellationToken cancellationToken);
    }
}