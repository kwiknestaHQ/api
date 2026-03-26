using KwikNesta.Mediator.Cores.Abstractions;
using KwikNesta.Shared.Responses;
using KwikNesta.Shared.ServiceDTOs.Identity;
using System.Text.Json.Serialization;

namespace KwikNesta.Shared.ServiceCommands.Identity
{
    public class LoginCommand : IKNRequest<Response<LoginResponseDto>>
    {
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;
        [JsonIgnore]
        public string? UserIpAddress { get; set; }
    }
}