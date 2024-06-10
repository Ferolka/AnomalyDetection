using AnomalyDetection.Extensions;
using AnomalyDetection.Interfaces;
using AnomalyDetection.Models;
using Api.ApiModels;
using Api.Endpoints;
using Api.Mapperly;
using Common.Entities;
using Db.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.ML;
using Microsoft.ML;
using Microsoft.ML.Transforms.TimeSeries;

namespace Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = this.Configuration.GetConnectionString("PostgresConnection");

            ArgumentException.ThrowIfNullOrEmpty(connectionString);

            services
                .AddDbServices(this.Configuration, connectionString)
                .AddAnomalyDetectionServices(this.Configuration);

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", () =>
                {
                    return Results.Ok("Anomaly detection v1.0.0");
                });

                endpoints
                    .MapGroup("/phrase")
                    .WithTags("Phrase")
                    .MapPhraseEndpoints();

                endpoints
                    .MapGroup("/ml")
                    .WithTags("ML")
                    .MapMLEndpoints();
            });
        }
    }
}