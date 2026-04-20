using KwikNesta.Shared.Models;
using KwikNesta.Shared.Models.Enumerations.Property;

namespace KwikNestaProperty.Domain.Entities
{
    public class ViewingSession : BaseEntity
    {
        public Guid ViewingRequestId { get; set; }
        public ViewingRequest ViewingRequest { get; set; } = default!;
        public string ChannelName { get; set; } = default!;
        public DateTime ScheduledStart { get; set; }
        public DateTime? ActualStart { get; set; }
        public DateTime? ActualEnd { get; set; }
        public int DurationSeconds { get; set; }
        public EViewingSessionStatus Status { get; set; } = EViewingSessionStatus.Scheduled;
        public ICollection<SessionParticipant> Participants { get; set; } = [];
    }
}