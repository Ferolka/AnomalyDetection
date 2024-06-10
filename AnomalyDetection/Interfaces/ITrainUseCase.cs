using Common.Models;

namespace AnomalyDetection.Interfaces
{
    public interface ITrainUseCase
    {
        Task<ResultModel> ExecuteAsync(CancellationToken cancellationToken);
    }
}