using KwikNesta.Mediator.Cores.Abstractions;
using KwikNesta.Mediator.Cores.Implementations;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace KwikNesta.Mediator.Cores.Extensions
{
    public static class KNDIExtensions
    {
        /// <summary>
        /// Registers KNMediator core services, handlers
        /// </summary>
        /// <param name="services">The service collection to configure.</param>
        /// <param name="assemblies">
        /// Optional assemblies to scan for handler implementations.
        /// If not provided, all currently loaded assemblies are scanned.
        /// </param>
        public static IServiceCollection ConfigureKNMediators(this IServiceCollection services, params Assembly[] assemblies)
        {
            services.AddScoped<IKNMediator, KNMediator>();
            if (assemblies == null || assemblies.Length == 0)
            {
                assemblies = AppDomain.CurrentDomain.GetAssemblies();
            }

            assemblies = assemblies
                .Concat(new[] { typeof(IKNMediator).Assembly })
                .Distinct()
                .ToArray();

            var handlerTypes = assemblies
                .SelectMany(a => a.GetTypes())
                .Where(t => !t.IsAbstract && !t.IsInterface &&
                           t.GetInterfaces().Any(i => i.IsGenericType &&
                               (i.GetGenericTypeDefinition() == typeof(IKNRequestHandler<,>) ||
                                i.GetGenericTypeDefinition() == typeof(IKNNotificationHandler<>)))
                );

            foreach (var type in handlerTypes)
            {
                foreach (var i in type.GetInterfaces())
                {
                    if (i.IsGenericType &&
                        (i.GetGenericTypeDefinition() == typeof(IKNRequestHandler<,>) ||
                         i.GetGenericTypeDefinition() == typeof(IKNNotificationHandler<>)))
                    {
                        services.AddTransient(i, type);
                    }
                }
            }

            return services;
        }
    }
}