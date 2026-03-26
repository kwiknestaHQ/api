using KwikNesta.Mediator.Cores.Abstractions;
using KwikNesta.Shared.Responses;

namespace KwikNesta.Shared.ServiceCommands.Infra
{
    public class ToggleCountryStatusCommand : IKNRequest<Response<string>>
    {
        public Guid Id { get; set; }
        public string LoggedInUserId { get; set; } = default!;
        public string LoggedInUserEmail { get; set; } = default!;
        public string? LoggedInUserIpAddress { get; set; }
    }
}
