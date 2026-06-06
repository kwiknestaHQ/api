using KwikNesta.Mediator.Cores.Abstractions;
using KwikNesta.Shared.Responses;

namespace KwikNesta.Shared.ServiceCommands.Infra
{
    public class CreateAgoraTokenCommand : IKNRequest<Response<string>>
    {
        public string Channel { get; set; } = default!;
        public string UserId { get; set; } = default!;
        public string Token { get; set; } = default!;
        public DateTime Expires { get; set; }
    }
}