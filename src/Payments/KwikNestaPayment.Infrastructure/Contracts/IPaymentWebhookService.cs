using Hangfire.Server;

namespace KwikNestaPayment.Infrastructure.Contracts
{
    public interface IPaymentWebhookService
    {
        Task ProcessPaystackWebhook(string body, PerformContext context);
    }
}