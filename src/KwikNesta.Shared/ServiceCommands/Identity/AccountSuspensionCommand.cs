using KwikNesta.Mediator.Cores.Abstractions;
using KwikNesta.Shared.Models.Enumerations.Identity;
using KwikNesta.Shared.Responses;

namespace KwikNesta.Shared.ServiceCommands.Identity
{
    public class AccountSuspensionCommand : IKNRequest<Response<string>>
    {
        public string UserId { get; set; } = default!;
        public SuspensionReasons Reason { get; set; }
        public string? OtherReason { get; set; }

        public string LoggedInUserId { get; set; } = default!;
        public string? LoggedInUserIpAddress { get; set; }
    }

    public class AccountSuspensionRequest
    {
        public string UserId { get; set; } = default!;
        public SuspensionReasons Reason { get; set; }
        public string? OtherReason { get; set; }
    }
}