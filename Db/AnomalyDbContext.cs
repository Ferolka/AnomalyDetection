using Common.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Db
{
    public class AnomalyDbContext : IdentityDbContext
    {
        public AnomalyDbContext(DbContextOptions<AnomalyDbContext> dbContextOptions)
            : base(dbContextOptions)
        {
        }

        public DbSet<Phrase> Phrases { get; set; }
        public DbSet<PredictedPhrase> PredictedPhrases { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}