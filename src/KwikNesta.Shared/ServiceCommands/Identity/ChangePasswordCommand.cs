using KwikNesta.Mediator.Cores.Abstractions;
using KwikNesta.Shared.Responses;
using System.Text.Json.Serialization;

namespace KwikNesta.Shared.ServiceCommands.Identity
{
    public class ChangePasswordCommand : IKNRequest<Response<string>>
    {
        [JsonIgnore]
        public string UserId { get; set; } = default!;
        public string CurrentPassword { get; set; } = default!;
        public string NewPassword { get; set; } = string.Empty;
        public string ConfirmNewPassword { get; set; } = string.Empty;
        [JsonIgnore]
        public string? UserIpAddress { get; set; }
    }
}