using KwikNesta.Mediator.Cores.Abstractions;
using KwikNesta.Shared.Responses;

namespace KwikNesta.Shared.ServiceCommands.Identity
{
    public class AccountRestoreCommand : IKNRequest<Response<string>>
    {
        public string UserId { get; set; } = default!;
        public string LoggedInUserId { get; set; } = default!;
        public string? LoggedInUserIpAddress { get; set; }
    }
}