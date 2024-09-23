using AnomalyDetection.Extensions;
using Api.Endpoints;
using Common.Entities;
using Db;
using Db.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;

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

            services.AddIdentityCore<IdentityUser>()
                            .AddEntityFrameworkStores<AnomalyDbContext>()
                            .AddApiEndpoints();

            services.AddAuthentication().AddBearerToken(IdentityConstants.BearerScheme);
            services.AddAuthorizationBuilder();

            services.Configure<IdentityOptions>(options =>
            {
                // Password settings
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = false;
                options.Password.RequiredUniqueChars = 4;

                // Lockout settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                options.Lockout.MaxFailedAccessAttempts = 10;
                options.Lockout.AllowedForNewUsers = true;

                // User settings
                options.User.RequireUniqueEmail = true;
            });

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter a valid token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type=ReferenceType.SecurityScheme,
                                Id="Bearer"
                            }
                        },
                        new string[]{}
                    }
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapIdentityApi<IdentityUser>();
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

            app.UseSwagger();
            app.UseSwaggerUI();
        }
    }
}