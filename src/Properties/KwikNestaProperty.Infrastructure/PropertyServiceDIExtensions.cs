using KwikNestaProperty.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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
                .AddScoped<IPropertyRepositotyManager, PropertyRepositotyManager>();
            return services;
        }
    }
}