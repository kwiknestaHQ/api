using Hangfire;
using KwikNestaProperty.Infrastructure.Data;
using KwikNestaProperty.Infrastructure.Services;
using KwikNestaProperty.Infrastructure.Services.Abstraction;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace KwikNestaProperty.Infrastructure
{
    public static class PropertyServiceDIExtensions
    {
        public static IServiceCollection ConfigurePropertyServices(this IServiceCollection services,
                                                                IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection") ??
                throw new ArgumentNullException("Connection string not set.");

            services.AddDbContext<PropertyServiceDbContext>(options =>
                options.UseNpgsql(connectionString))
                .AddScoped<IPropertyRepositoryManager, PropertyRepositoryManager>()
                .AddScoped<IPropertyBackgroundService, PropertyBackgroundService>();
            return services;
        }

        public static WebApplication RunPropertyServiceMigrations(this WebApplication app)
        {
            if (!app.Environment.IsDevelopment())
            {
                using var scope = app.Services.CreateScope();
                var db = scope.ServiceProvider.GetRequiredService<PropertyServiceDbContext>();
                db.Database.Migrate();
            }

            return app;
        }

        public static WebApplication RegisterPropertyRecurringJobs(this WebApplication app)
        {
            app.Lifetime.ApplicationStarted
                .Register(() =>
                {
                    RecurringJob.AddOrUpdate<IPropertyBackgroundService>(
                        "RunSettlementsInitiationAsync",
                        x => x.RunSettlementsInitiationAsync(null!, CancellationToken.None), Cron.Hourly);
                });

            return app;
        }
    }
}