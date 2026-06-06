using Hangfire.Server;

namespace KwikNestaProperty.Infrastructure.Services.Abstraction
{
    public interface IPropertyBackgroundService
    {
        Task RunSettlementsInitiationAsync(PerformContext context, CancellationToken cancellationToken);
        Task VerifyLocation(Guid propertyId, PerformContext context);
    }
}