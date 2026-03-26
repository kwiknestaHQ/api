using KwikNesta.Mediator.Cores.Abstractions;
using KwikNesta.Shared.Responses;

namespace KwikNesta.Shared.ServiceCommands.Identity
{
    public class AccountReactivationCommand : IKNRequest<Response<string>>
    {
        public string Email { get; set; } = default!;
        public string Otp { get; set; } = default!;
    }
}