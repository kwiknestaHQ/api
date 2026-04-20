using KwikNesta.Shared.Models;
using KwikNesta.Shared.Models.Enumerations.Property;

namespace KwikNestaProperty.Domain.Entities
{
    public class ViewingRequest : BaseEntity
    {
        public Guid PropertyId { get; set; }
        public KNProperty Property { get; set; } = default!;
        public string UserId { get; set; } = default!;
        public decimal Fee { get; set; }
        public DateTime RequestedDate { get; set; }
        public EViewingStatus Status { get; set; } = EViewingStatus.Pending;
        public EViewingPaymentStatus PaymentStatus { get; set; } = EViewingPaymentStatus.Pending;
        public DateTime? RespondedAt { get; set; }
        public EViewingType Type { get; set; }
        public string? Note { get; set; }

        public Guid? SessionId { get; set; }
        public ViewingSession Session { get; set; } = default!;
    }
}