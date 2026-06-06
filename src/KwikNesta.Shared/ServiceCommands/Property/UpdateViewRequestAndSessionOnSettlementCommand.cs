using KwikNesta.Mediator.Cores.Abstractions;
using KwikNesta.Shared.Responses;

namespace KwikNesta.Shared.ServiceCommands.Property
{
    public class UpdateViewRequestAndSessionOnSettlementCommand : 
        IKNRequest<Response<string>>
    {
        public Guid ViewRequestId { get; set; }
    }
}