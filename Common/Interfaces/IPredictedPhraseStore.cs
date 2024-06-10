using Common.Entities;
using Common.Models;

namespace Common.Interfaces
{
    public interface IPredictedPhraseStore
    {
        Task<ResultModel<long>> AddPredictedPhraseIfNotExistsAsync(PredictedPhrase predictedPhrase,
            CancellationToken cancellationToken);

        Task<ResultModel<List<PredictedPhrase>>> GetUncheckedPredictedPhrasesAsync(
            int top,
            int skip,
            CancellationToken cancellationToken);

        Task<ResultModel> UpdatePredictedPhrasesAsync(
            List<PredictedPhrase> predictedPhrases,
            CancellationToken cancellationToken);
    }
}