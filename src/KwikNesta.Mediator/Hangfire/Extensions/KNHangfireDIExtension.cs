using KwikNesta.Mediator.Hangfire.Abstractions;
using KwikNesta.Mediator.Hangfire.Implementations;
using Microsoft.Extensions.DependencyInjection;

namespace KwikNesta.Mediator.Hangfire.Extensions
{
    public static class KNHangfireDIExtension
    {
        public static IServiceCollection ConfigureKNBackgroundMediators(this IServiceCollection services)
        {
            services.AddScoped<IKNBackgroundMediator, KNBackgroundMediator>();
            return services;
        }
    }
}