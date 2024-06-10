using Common.Interfaces;
using Common.Models;

namespace Db.Stores
{
    internal class UnitOfWorkStore : IUnitOfWorkStore
    {
        private readonly AnomalyDbContext _dbContext;

        public UnitOfWorkStore(AnomalyDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public void AddRange<T>(IEnumerable<T> values)
        {
            foreach (var value in values)
            {
                this._dbContext.Add(value!);
            }
        }

        public void UpdateRange<T>(IEnumerable<T> values)
        {
            foreach (var value in values)
            {
                this._dbContext.Update(value!);
            }
        }

        public async Task<ResultModel<int>> SaveChangesAsync(CancellationToken cancellationToken)
        {
            try
            {
                var count = await this._dbContext.SaveChangesAsync(cancellationToken);
                return ResultModel<int>.FromSuccess(count);
            }
            catch (Exception ex)
            {
                return ResultModel<int>.FromDbError(ex);
            }
        }
    }
}