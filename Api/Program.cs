using Db;
using Serilog;

namespace Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<AnomalyDbContext>();
                    DbInitializer.Initialize(context, services);
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while seeding the database.");
                }
            }
            host.Run();
        }

        public static IHostBuilder CreateWebHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog(
                    (context, configuration) =>
                    {
                        configuration.ReadFrom.Configuration(context.Configuration);
                    }, true)
                .ConfigureWebHostDefaults(webBuilder =>
                    {
                        webBuilder.ConfigureKestrel(serverOptions =>
                        {
                            serverOptions.AddServerHeader = false;
                        })
                        .UseStartup<Startup>();
                    });
    }
}