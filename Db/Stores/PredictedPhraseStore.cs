using Common.Entities;
using Common.Interfaces;
using Common.Models;
using Microsoft.EntityFrameworkCore;

namespace Db.Stores
{
    internal class PredictedPhraseStore : IPredictedPhraseStore
    {
        private readonly AnomalyDbContext _dbContext;

        public PredictedPhraseStore(AnomalyDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<ResultModel<long>> AddPredictedPhraseIfNotExistsAsync(PredictedPhrase predictedPhrase,
            CancellationToken cancellationToken)
        {
            try
            {
                var dbPredictedPhrase = await this._dbContext.PredictedPhrases
                    .FirstOrDefaultAsync(x => EF.Functions.ILike(x.Text, predictedPhrase.Text),
                        cancellationToken);
                if (dbPredictedPhrase is not null)
                {
                    return ResultModel<long>.FromSuccess(dbPredictedPhrase.Id);
                }

                this._dbContext.Add(predictedPhrase);
                await this._dbContext.SaveChangesAsync(cancellationToken);

                return ResultModel<long>.FromSuccess(predictedPhrase.Id);
            }
            catch (Exception ex)
            {
                return ResultModel<long>.FromDbError(ex);
            }
        }
        public async Task<ResultModel<List<PredictedPhrase>>> GetUncheckedPredictedPhrasesAsync(
            int top,
            int skip,
            CancellationToken cancellationToken)
        {
            try
            {
                var result = await this._dbContext.PredictedPhrases
                    .AsNoTracking()
                    .OrderBy(x => x.Id)
                    .Where(x => !x.Checked)
                    .Take(top)
                    .Skip(skip)
                    .ToListAsync(cancellationToken);

                return ResultModel<List<PredictedPhrase>>.FromSuccess(result);
            }
            catch (Exception ex)
            {
                return ResultModel<List<PredictedPhrase>>.FromDbError(ex);
            }
        }

        public async Task<ResultModel> UpdatePredictedPhrasesAsync(
            List<PredictedPhrase> predictedPhrases,
            CancellationToken cancellationToken)
        {
            try
            {
                this._dbContext.UpdateRange(predictedPhrases);
                await this._dbContext.SaveChangesAsync(cancellationToken);
                return ResultModel.FromSuccess();
            }
            catch (Exception ex)
            {
                return ResultModel.FromDbError(ex);
            }
        }
    }
}