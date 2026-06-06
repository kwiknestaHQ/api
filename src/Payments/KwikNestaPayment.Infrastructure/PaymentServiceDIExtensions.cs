using Hangfire;
using KwikNestaPayment.Infrastructure.Contracts;
using KwikNestaPayment.Infrastructure.Data;
using KwikNestaPayment.Infrastructure.Services;
using KwikNestaPayment.Infrastructure.Services.Background;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace KwikNestaPayment.Infrastructure
{
    public static class PaymentServiceDIExtensions
    {
        public static IServiceCollection ConfigurePaymentInfraServices(this IServiceCollection services,
                                                                IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection") ??
                throw new ArgumentNullException("Connection string not set.");

            services.AddDbContext<PaymentServiceDbContext>(options =>
                    options.UseNpgsql(connectionString))
                .AddScoped<IPaymentRepositoryManager, PaymentRepositoryManager>()
                .AddScoped<IPaymentWebhookService, PaymentWebhookService>()
                .AddScoped<IPaystackWebhookHandler, ChargeSuccessHandler>()
                .AddScoped<IPaystackWebhookHandler, ChargeFailedHandler>()
                .AddScoped<IPaystackWebhookHandler, TransferSuccessHandler>()
                .AddScoped<IPaystackWebhookHandler, TransferFailedHandler>()
                .AddScoped<IPaymentBackgroundService, PaymentBackgroundService>()
                .AddScoped<PaystackWebhookDispatcher>()
                .AddScoped<PaymentRouter>();
            return services;
        }

        public static WebApplication RunPaymentServiceMigrations(this WebApplication app)
        {
            if (!app.Environment.IsDevelopment())
            {
                using var scope = app.Services.CreateScope();
                var db = scope.ServiceProvider.GetRequiredService<PaymentServiceDbContext>();
                db.Database.Migrate();
            }

            return app;
        }

        public static WebApplication RegisterPaymentRecurringJobs(this WebApplication app)
        {
            app.Lifetime.ApplicationStarted
                .Register(() =>
                {
                    RecurringJob.AddOrUpdate<IPaymentBackgroundService>(
                        "RunPaymentVerificationsAsync", 
                        x => x.RunPaymentVerificationsAsync(null!, CancellationToken.None), "*/5 * * * *");

                    RecurringJob.AddOrUpdate<IPaymentBackgroundService>(
                        "ProcessSettlementsAsync", 
                        x => x.ProcessSettlementsAsync(null!, CancellationToken.None), "*/5 * * * *");
                });

            return app;
        }
    }
}
