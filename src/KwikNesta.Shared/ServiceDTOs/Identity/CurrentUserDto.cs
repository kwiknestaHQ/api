using KwikNesta.Shared.Extensions;
using KwikNesta.Shared.Models.Enumerations.Identity;

namespace KwikNesta.Shared.ServiceDTOs.Identity
{
    public class CurrentUserDto
    {
        public string Id { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string? MiddleName { get; set; }
        public string PhoneNumber { get; set; } = default!;
        public EGender Gender { get; set; }
        public string GenderText => Gender.GetDescription();
        public EUserStatus Status { get; set; }
        public string StatusText => Status.GetDescription();
    }
}
