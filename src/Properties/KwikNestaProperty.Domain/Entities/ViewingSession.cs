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
        public ESSessionettlementStatus SetlementStatus { get; set; } = ESSessionettlementStatus.Pending;
        public EViewingSessionStatus Status { get; set; } = EViewingSessionStatus.Scheduled;
        public ICollection<SessionParticipant> Participants { get; set; } = [];

        public void MarkAsInProgress(DateTime time)
        {
            ActualStart ??= time;
            LastUpdatedOn = DateTime.UtcNow;
            Status = EViewingSessionStatus.InProgress;
        }

        public void MarkAsCompleted(DateTime time)
        {
            ActualEnd ??= time;
            LastUpdatedOn = DateTime.UtcNow;
            Status = EViewingSessionStatus.Completed;
        }

        public bool IsExpired(long durationSeconds)
        {
            return ScheduledStart.AddSeconds(durationSeconds) > DateTime.UtcNow;
        }

        public bool IsSettled()
        {
            return SetlementStatus == ESSessionettlementStatus.Scheduled ||
                SetlementStatus == ESSessionettlementStatus.Completed;
        }

        public void UpdateSettlementStatus(ESSessionettlementStatus status)
        {
            SetlementStatus = status;
            LastUpdatedOn = DateTime.UtcNow;
        }
    }
}