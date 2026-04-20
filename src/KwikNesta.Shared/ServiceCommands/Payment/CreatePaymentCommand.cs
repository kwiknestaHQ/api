using KwikNesta.Mediator.Cores.Abstractions;
using KwikNesta.Shared.Models;
using KwikNesta.Shared.Models.Enumerations.Payments;
using KwikNesta.Shared.Responses;
using KwikNesta.Shared.ServiceDTOs.Payment;

namespace KwikNesta.Shared.ServiceCommands.Payment
{
    public class CreatePaymentCommand : IKNRequest<Response<PaymentDto>>
    {
        public UserContext Context { get; set; } = default!;
        public Guid ReferenceId { get; set; }
        public string Reference { get; set; } = default!;
        public decimal Amount { get; set; }
        public decimal PlatformFee { get; set; }
        public decimal NetAmount { get; set; }
        public EPaymentPurpose Purpose { get; set; }
    }
}