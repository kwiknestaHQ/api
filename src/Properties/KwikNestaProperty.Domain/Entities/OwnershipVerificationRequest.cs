using KwikNesta.Shared.Models;
using KwikNesta.Shared.Models.Enumerations.Property;

namespace KwikNestaProperty.Domain.Entities
{
    public class OwnershipVerificationRequest : BaseEntity
    {
        public Guid PropertyId { get; set; }
        public KNProperty Property { get; set; } = default!;
        public EVerificationStatus Status { get; set; } = EVerificationStatus.Pending;
        public string? RejectionReason { get; set; }
        public DateTime? ReviewedAt { get; set; }
        public ICollection<OwnershipDocument> Documents { get; set; } = [];
    }
}