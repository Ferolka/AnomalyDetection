using AnomalyDetection.Models;
using Common.Entities;
using Common.Models;

namespace AnomalyDetection.Interfaces
{
    public interface IPredictedPhraseUseCase
    {
        Task<ResultModel<List<PredictedPhrase>>> GetUncheckedPredictedPhrases(
            PaginatedModel paginatedModel,
            CancellationToken cancellationToken);
        Task<ResultModel> VerifyPredictedPhrases(
            List<PredictedPhrase> predictedPhrases,
            CancellationToken cancellationToken);
    }
}