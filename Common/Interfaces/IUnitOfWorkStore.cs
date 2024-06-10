using Common.Models;

namespace Common.Interfaces
{
    public interface IUnitOfWorkStore
    {
        public void UpdateRange<T>(IEnumerable<T> values);

        public void AddRange<T>(IEnumerable<T> values);

        public Task<ResultModel<int>> SaveChangesAsync(CancellationToken cancellationToken);
    }
}