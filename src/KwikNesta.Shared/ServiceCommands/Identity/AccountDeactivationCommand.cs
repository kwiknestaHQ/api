using KwikNesta.Mediator.Cores.Abstractions;
using KwikNesta.Shared.Responses;
using System.Text.Json.Serialization;

namespace KwikNesta.Shared.ServiceCommands.Identity
{
    public class AccountDeactivationCommand : IKNRequest<Response<string>>
    {
        public string UserId { get; set; } = default!;
        public string LoggedInUserId { get; set; } = default!;
        public string LoggedInUserEmail { get; set; } = default!;
        public string? UserIpAddress { get; set; }
        [JsonIgnore]
        public bool Self => UserId.Equals(LoggedInUserId);
    }
}