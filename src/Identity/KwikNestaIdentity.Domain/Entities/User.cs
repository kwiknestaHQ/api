using KwikNesta.Shared.Models.Enumerations.Identity;
using Microsoft.AspNetCore.Identity;

namespace KwikNestaIdentity.Domain.Entities
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? OtherName { get; set; }
        public EGender Gender { get; set; }
        public EUserStatus Status { get; set; } = EUserStatus.PendingVerification;
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public DateTime? LastUpdatedOn { get; set; }
        public DateTime? LastLogin { get; set; }
        public DateTime? StatusChangedAt { get; set; }
    }
}