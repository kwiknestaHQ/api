using KwikNesta.Mediator.Cores.Abstractions;
using KwikNesta.Shared.Responses;
using KwikNesta.Shared.ServiceDTOs.Payment;

namespace KwikNesta.Shared.ServiceCommands.Payment
{
    public class InitiateTransferCommand : IKNRequest<Response<InitiateTransferResponse>>
    {
        public decimal Amount { get; set; }
        public string RecipientCode { get; set; } = default!;
        public string Narration { get; set; } = default!;
    }
}