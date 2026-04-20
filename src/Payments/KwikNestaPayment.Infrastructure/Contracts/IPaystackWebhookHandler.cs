using KwikNesta.Shared.ServiceDTOs.Payment;

namespace KwikNestaPayment.Infrastructure.Contracts
{
    public interface IPaystackWebhookHandler
    {
        string EventType { get; }
        Task HandleAsync(PaystackWebhookDto payload);
    }
}