using KwikNesta.Shared.Models;
using KwikNesta.Shared.Models.Enumerations.Property;

namespace KwikNestaProperty.Domain.Entities
{
    public class SessionParticipant : BaseEntity
    {
        public Guid ViewingSessionId { get; set; }
        public ViewingSession ViewingSession { get; set; } = default!;
        public string UserId { get; set; } = default!;
        public uint UId { get; set; }
        public ESessionParticipantRole Role { get; set; }
        public DateTime? JoinedAt { get; set; }
        public DateTime? LeftAt { get; set; }
        public long DurationSeconds { get; set; }

        public void MarkAsJoined(DateTime time)
        {
            JoinedAt ??= time;
            LastUpdatedOn = DateTime.UtcNow;
        }

        public void MarkAsLeft(DateTime time, long duration)
        {
            LeftAt = time;
            DurationSeconds += duration;
            LastUpdatedOn = DateTime.UtcNow;
        }
    }
}
