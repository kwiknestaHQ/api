using KwikNesta.Shared.Models;
using KwikNesta.Shared.Models.Enumerations.Identity;

namespace KwikNestaIdentity.Domain.Entities
{
    public class OtpEntry : BaseEntity
    {
        public string UserId { get; set; } = default!;
        public User? User { get; set; }

        public string OtpHash { get; set; } = default!;
        public DateTime ExpiresAt { get; set; }
        public EOtpType Type { get; set; }
        public string? TokenHash { get; set; } = default!;
        public int Attempts { get; set; }
    }
}