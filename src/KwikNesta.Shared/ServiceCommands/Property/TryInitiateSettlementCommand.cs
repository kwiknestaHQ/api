using KwikNesta.Mediator.Cores.Abstractions;
using KwikNesta.Shared.Responses;

namespace KwikNesta.Shared.ServiceCommands.Property
{
    public class TryInitiateSettlementCommand : IKNRequest<Response<string>>
    {
        public string Channel { get; set; } = default!;
    }
}