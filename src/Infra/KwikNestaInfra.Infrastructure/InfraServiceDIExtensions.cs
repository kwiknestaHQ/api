using KwikNesta.Shared.Contracts;
using KwikNesta.Shared.Implementations;
using KwikNesta.Shared.Models.Settings;
using KwikNestaInfra.Infrastructure.Contracts;
using KwikNestaInfra.Infrastructure.Data;
using KwikNestaInfra.Infrastructure.External;
using KwikNestaInfra.Infrastructure.Services;
using KwikNestaInfra.Infrastructure.Services.AgoraHandlers.Inspection;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace KwikNestaInfra.Infrastructure
{
    public static class InfraServiceDIExtensions
    {
        public static IServiceCollection ConfigureInfraServices(this IServiceCollection services,
                                                            IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection") ??
                throw new ArgumentNullException("Connection string not set.");

            services.AddDbContext<InfraServiceDbContext>(options =>
                options.UseNpgsql(connectionString));

            return services.AddScoped<IInfraRepositoryManager, InfraRepositoryManager>()
                .AddScoped<IAppAuditService, AppAuditService>()
                .AddScoped<ICsApiService, CsApiService>()
                .ConfigureHttpClients(configuration)
                .ConfigureAgoraServices();
        }

        public static WebApplication RunInfraServiceMigrations(this WebApplication app)
        {
            if (!app.Environment.IsDevelopment())
            {
                using var scope = app.Services.CreateScope();
                var db = scope.ServiceProvider.GetRequiredService<InfraServiceDbContext>();
                db.Database.Migrate();
            }

            return app;
        }

        private static IServiceCollection ConfigureAgoraServices(this IServiceCollection services)
        {
            return services
                .AddScoped<IVirtualMediaCallService, VirtualMediaCallService>()
                .AddScoped<AgoraWebhookEventDispatcher>()
                .AddScoped<IAgoraModuleHandler, ChannelCreatedHandler>()
                .AddScoped<IAgoraModuleHandler, ChannelDestroyedHandler>()
                .AddScoped<IAgoraModuleHandler, HostJoinsHandler>()
                .AddScoped<IAgoraModuleHandler, HostLeavesHandler>()
                .AddScoped<IAgoraModuleHandler, AudienceJoinsHandler>()
                .AddScoped<IAgoraModuleHandler, AudienceLeavesHandler>();
        }

        private static IServiceCollection ConfigureHttpClients(this IServiceCollection services, 
                                                            IConfiguration configuration)
        {
            var setting = configuration.GetSection("CsApi").Get<CsApiSettings>() ?? 
                throw new ArgumentNullException(nameof(CsApiSettings));

            services.AddHttpClient("CsApi", client =>
            {
                client.BaseAddress = new Uri(setting.BaseUrl);
                client.DefaultRequestHeaders.Add("X-CSCAPI-KEY", setting.ApiKey);
            });

            return services;
        }
    }
}