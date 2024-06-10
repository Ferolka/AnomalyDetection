using Common.Entities;
using Common.Interfaces;
using Common.Models;
using Microsoft.EntityFrameworkCore;

namespace Db.Stores
{
    internal class PhraseStore : IPhraseStore
    {
        private readonly AnomalyDbContext _dbContext;

        public PhraseStore(AnomalyDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<ResultModel<List<long>>> AddPhrasesAsync(List<Phrase> toxicWords,
            CancellationToken cancellationToken)
        {
            try
            {
                this._dbContext.AddRange(toxicWords);
                await this._dbContext.SaveChangesAsync(cancellationToken);
                return ResultModel<List<long>>.FromSuccess(toxicWords.ConvertAll(x => x.Id));
            }
            catch (Exception ex)
            {
                return ResultModel<List<long>>.FromDbError(ex);
            }
        }

        public async Task<ResultModel<List<Phrase>>> GetPhrasesAsync(
            CancellationToken cancellationToken,
            int limit = 1000)
        {
            try
            {
                var result = await this._dbContext.Phrases
                    .AsNoTracking()
                    .OrderByDescending(x => x.Id)
                    .Take(limit)
                    .ToListAsync(cancellationToken);

                return ResultModel<List<Phrase>>.FromSuccess(result);
            }
            catch (Exception ex)
            {
                return ResultModel<List<Phrase>>.FromDbError(ex);
            }
        }
    }
}