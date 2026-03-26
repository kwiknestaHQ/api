using KwikNesta.Mediator.Cores.Abstractions;
using KwikNesta.Shared.Responses;

namespace KwikNesta.Shared.ServiceCommands.Identity
{
    public class AccountReactivationRequestCommand : IKNRequest<Response<string>>
    {
        public string Email { get; set; } = default!;
    }
}