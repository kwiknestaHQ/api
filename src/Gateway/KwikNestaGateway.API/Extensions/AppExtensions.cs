using Hangfire;
using KwikNesta.Shared.Models.Settings;
using KwikNestaGateway.API.Filters;
using KwikNestaGateway.API.Middlewares;
using KwikNestaIdentity.Infrastructure;
using KwikNestaInfra.Infrastructure;
using Microsoft.AspNetCore.HttpOverrides;

namespace KwikNestaGateway.API.Extensions
{
    public static class AppExtensions
    {
        public static IApplicationBuilder UseMiddlewares(this WebApplication app, IConfiguration config)
        {

            app.UseSwaggerDocsUI();
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto,
                KnownNetworks = { },
                KnownProxies = { }
            });

            app.UseMiddleware<RequestLoggingMiddleware>();

            app.UseCors("CorsPolicy");

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseMiddleware<ActiveUserMiddleware>();
            app.UseAuthorization();

            app.AddUseHangfireDashboard(config);

            app.MapControllers();

            app.RunMigrations();
            app.RunDataSeedAsync().Wait();
            return app;
        }

        static WebApplication RunMigrations(this WebApplication app)
        {

            return app.RunIdentityServiceMigrations()
                .RunInfraServiceMigrations();        
        }

        static async Task<IHost> RunDataSeedAsync(this IHost host)
        {
            await host.SeedIdentityData();

            return host;
        }

        static WebApplication UseSwaggerDocsUI(this WebApplication app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(opt =>
            {
                opt.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                opt.SwaggerEndpoint("/swagger/v2/swagger.json", "v2");
            });

            return app;
        }

        private static WebApplication AddUseHangfireDashboard(this WebApplication app, IConfiguration configuration)
        {
            var settings = configuration.GetSection("Hangfire")
                        .Get<HangfireSettings>();

            var userName = settings?.UserName;
            var pass = settings?.Password;
            if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(pass))
            {
                throw new ArgumentNullException();
            }

            app.UseHangfireDashboard("/admin/jobs", new DashboardOptions
            {
                Authorization = new[] { new HangfireAuthFilter(userName, pass) },
                DashboardTitle = "Kwik Nesta API",
                DisplayStorageConnectionString = false,
                DisplayNameFunc = (_, job) => job.Method.Name,
            });

            return app;
        }
    }
}