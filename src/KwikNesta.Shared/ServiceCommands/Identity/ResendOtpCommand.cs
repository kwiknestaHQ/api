using KwikNesta.Mediator.Cores.Abstractions;
using KwikNesta.Shared.Models.Enumerations.Identity;
using KwikNesta.Shared.Responses;

namespace KwikNesta.Shared.ServiceCommands.Identity
{
    public class ResendOtpCommand : IKNRequest<Response<string>>
    {
        public string Email { get; set; } = default!;
        public EOtpType Type { get; set; }
    }
}