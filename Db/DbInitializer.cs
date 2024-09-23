using CsvHelper;
using Db.Csv.Mapperly;
using Db.Csv.Models;
using Microsoft.AspNetCore.Identity;
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
                var userManager = service.GetRequiredService<UserManager<IdentityUser>>();
                SeedDatabase(context, logger);
                CreateAdminRole(context, userManager);
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

        private static void CreateAdminRole(AnomalyDbContext context, UserManager<IdentityUser> userManager)
        {
            var user = new IdentityUser()
            {
                UserName = "admin",
                Email = "admin@default.com",
            };

            string adminPassword = "Password123";
            var userResult = userManager.CreateAsync(user, adminPassword).GetAwaiter().GetResult();

            if (userResult.Succeeded)
            {
                userManager.AddToRoleAsync(user, "Admin").Wait();
            }
        }
    }
}