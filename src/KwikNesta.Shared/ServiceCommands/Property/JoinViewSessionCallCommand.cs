using KwikNesta.Mediator.Cores.Abstractions;
using KwikNesta.Shared.Models;
using KwikNesta.Shared.Responses;
using KwikNesta.Shared.ServiceDTOs.Property;

namespace KwikNesta.Shared.ServiceCommands.Property
{
    public class JoinViewSessionCallCommand : IKNRequest<Response<AgoraSessionTokenDto>>
    {
        public Guid SessionId { get; set; }
        public UserContext Context { get; set; } = default!;
    }
}