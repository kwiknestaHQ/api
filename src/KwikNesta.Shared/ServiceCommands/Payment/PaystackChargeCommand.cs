using KwikNesta.Mediator.Cores.Abstractions;
using KwikNesta.Shared.Models;
using KwikNesta.Shared.Responses;
using KwikNesta.Shared.ServiceDTOs.Payment;

namespace KwikNesta.Shared.ServiceCommands.Payment
{
    public class PaystackChargeCommand : IKNRequest<Response<PaystackInitResult>>
    {
        public UserContext Context { get; set; } = default!;
        public string Email { get; set; } = default!;
        public decimal Amount { get; set; }
        public string Reference { get; set; } = default!;
    }
}