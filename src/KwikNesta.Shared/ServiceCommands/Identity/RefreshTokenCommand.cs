using KwikNesta.Mediator.Cores.Abstractions;
using KwikNesta.Shared.Responses;
using KwikNesta.Shared.ServiceDTOs.Identity;

namespace KwikNesta.Shared.ServiceCommands.Identity
{
    public class RefreshTokenCommand : IKNRequest<Response<LoginResponseDto>>
    {
        public string RefreshToken { get; set; } = default!;
    }
}