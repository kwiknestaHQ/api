using Hangfire.Server;

namespace KwikNestaPayment.Infrastructure.Contracts
{
    public interface IPaymentBackgroundService
    {
        Task ProcessSettlementsAsync(PerformContext context, CancellationToken cancellationToken);
        Task RunPaymentVerificationsAsync(PerformContext context, CancellationToken cancellationToken);
    }
}