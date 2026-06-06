using KwikNesta.Mediator.Cores.Abstractions;
using KwikNesta.Shared.Responses;

namespace KwikNesta.Shared.ServiceCommands.Identity
{
    public class RecordNoShowCommand : IKNRequest<Response<string>>
    {
        public string UserId { get; set; } = default!;
    }
}