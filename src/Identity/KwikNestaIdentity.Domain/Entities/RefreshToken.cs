using KwikNesta.Shared.Models;

namespace KwikNestaIdentity.Domain.Entities
{
    public class RefreshToken : BaseEntity
    {
        public string TokenHash { get; set; } = default!;
        public string UserId { get; set; } = default!;
        public User? User { get; set; }
        public DateTimeOffset ExpiresAt { get; set; }
        public DateTimeOffset? RevokedAt { get; set; }
    }
}