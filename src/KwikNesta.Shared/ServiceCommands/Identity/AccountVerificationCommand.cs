using KwikNesta.Mediator.Cores.Abstractions;
using KwikNesta.Shared.Responses;

namespace KwikNesta.Shared.ServiceCommands.Identity
{
    public class AccountVerificationCommand : IKNRequest<Response<string>>
    {
        public string Otp { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}