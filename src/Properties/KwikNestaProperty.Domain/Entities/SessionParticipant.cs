using KwikNesta.Shared.Models;
using KwikNesta.Shared.Models.Enumerations.Property;

namespace KwikNestaProperty.Domain.Entities
{
    public class SessionParticipant : BaseEntity
    {
        public Guid ViewingSessionId { get; set; }
        public ViewingSession ViewingSession { get; set; } = default!;
        public string UserId { get; set; } = default!;
        public ESessionParticipantRole Role { get; set; }
        public DateTime? JoinedAt { get; set; }
        public DateTime? LeftAt { get; set; }
        public int DurationSeconds { get; set; }
    }
}
