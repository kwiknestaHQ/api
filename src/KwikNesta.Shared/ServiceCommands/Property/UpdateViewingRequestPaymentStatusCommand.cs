using KwikNesta.Mediator.Cores.Abstractions;
using KwikNesta.Shared.Models.Enumerations.Property;
using KwikNesta.Shared.Responses;

namespace KwikNesta.Shared.ServiceCommands.Property
{
    public class UpdateViewingRequestPaymentStatusCommand : IKNRequest<Response<string>>
    {
        public Guid Id { get; set; }
        public EViewingPaymentStatus NewStatus { get; set; }
    }
}
