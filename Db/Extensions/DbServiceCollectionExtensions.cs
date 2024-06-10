using Common.Interfaces;
using Db.Stores;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Db.Extensions
{
    public static class DbServiceCollectionExtensions
    {
        public static IServiceCollection AddDbServices(
            this IServiceCollection services,
            IConfiguration configuration,
            string connectionString)
        {
            services.AddDbContext<AnomalyDbContext>(builder => builder.UseNpgsql(
                connectionString, npSqlBuilder =>
                {
                    npSqlBuilder.EnableRetryOnFailure();
                }));

            services.AddScoped<IPhraseStore, PhraseStore>();
            services.AddScoped<IPredictedPhraseStore, PredictedPhraseStore>();
            services.AddScoped<IUnitOfWorkStore, UnitOfWorkStore>();

            return services;
        }
    }
}