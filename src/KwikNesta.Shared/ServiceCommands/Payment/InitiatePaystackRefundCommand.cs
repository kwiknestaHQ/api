using KwikNesta.Mediator.Cores.Abstractions;
using KwikNesta.Shared.Responses;
using KwikNesta.Shared.ServiceDTOs.Payment;

namespace KwikNesta.Shared.ServiceCommands.Payment
{
    public class InitiatePaystackRefundCommand : IKNRequest<Response<PaystackRefundInitiationResponse>>
    {
        public decimal Amount { get; set; }
        public string PaymentReference { get; set; } = default!;
        public string UserId { get; set; } = default!;
        public string UserEmail { get; set; } = default!;
    }
}