using CsvHelper;
using Db.Csv.Mapperly;
using Db.Csv.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Globalization;

namespace Db
{
    public class DbInitializer
    {
        private const string DataSetFolder = "Data";

        public static void Initialize(AnomalyDbContext context, IServiceProvider service)
        {
            if (context.Database.EnsureCreated())
            {
                var logger = service.GetRequiredService<ILogger<DbInitializer>>();
                SeedDatabase(context, logger);
            }
        }

        private static void SeedDatabase(AnomalyDbContext context, ILogger<DbInitializer> logger)
        {
            //https://github.com/surge-ai/toxicity/blob/main/toxicity_en.csv
            var baseDir = Directory.GetCurrentDirectory();
            var toxicDataSetDir = Path.GetFullPath(Path.Combine(baseDir, "..\\", "Db", DataSetFolder));

            if (!Directory.Exists(toxicDataSetDir))
            {
                return;
            }
            var dataSetsPerDir = Directory.GetFiles(toxicDataSetDir);

            foreach (var dataSetFile in dataSetsPerDir)
            {
                var dataSetPath = Path.Combine(toxicDataSetDir, dataSetFile);
                using var reader = new StreamReader(dataSetPath);
                using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
                var records = csv.GetRecords<ToxicWordCsv>();

                context.Phrases.AddRange(ToxicWordCsvMapperly.Map(records));
                context.SaveChanges();
            }
        }
    }
}