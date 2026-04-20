using KwikNestaProperty.Infrastructure.Data;
using KwikNestaProperty.Infrastructure.Services;
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
                .AddScoped<IPropertyRepositotyManager, PropertyRepositotyManager>()
                .AddScoped<BackgroundLocationVerificationService>();
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
    }
}