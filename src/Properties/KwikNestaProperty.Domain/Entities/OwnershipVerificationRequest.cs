using KwikNesta.Shared.Models;
using KwikNesta.Shared.Models.Enumerations.Property;

namespace KwikNestaProperty.Domain.Entities
{
    public class OwnershipVerificationRequest : BaseEntity
    {
        public Guid PropertyId { get; set; }
        public KNProperty Property { get; set; } = default!;
        public string OwnerId { get; set; } = default!;
        public EVerificationStatus Status { get; set; } = EVerificationStatus.Pending;
        public string? AdminComment { get; set; }
    }
}