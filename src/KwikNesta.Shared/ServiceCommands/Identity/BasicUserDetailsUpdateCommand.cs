using KwikNesta.Mediator.Cores.Abstractions;
using KwikNesta.Shared.Models.Enumerations.Identity;
using KwikNesta.Shared.Responses;

namespace KwikNesta.Shared.ServiceCommands.Identity
{
    public class BasicUserDetailsUpdateCommand : IKNRequest<Response<string>>
    {
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string? OtherName { get; set; }
        public EGender Gender { get; set; }
        public string LoggedInUserId { get; set; } = default!;
        public string? LoggedInUserIpAddress { get; set; }
    }

    public class BasicUserDetailsUpdateRequest
    {
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string? OtherName { get; set; }
        public EGender Gender { get; set; }
    }
}