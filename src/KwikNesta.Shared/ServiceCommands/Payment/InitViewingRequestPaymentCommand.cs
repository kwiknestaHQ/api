using KwikNesta.Mediator.Cores.Abstractions;
using KwikNesta.Shared.Models;
using KwikNesta.Shared.Responses;
using KwikNesta.Shared.ServiceDTOs.Payment;

namespace KwikNesta.Shared.ServiceCommands.Payment
{
    public class InitViewingRequestPaymentCommand : IKNRequest<Response<ViewRequestPaymentInitResult>>
    {
        public Guid Id { get; set; }
        public UserContext Context { get; set; } = default!;
    }
}