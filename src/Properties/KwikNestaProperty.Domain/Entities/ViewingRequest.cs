using KwikNesta.Shared.Models;
using KwikNesta.Shared.Models.Enumerations.Property;

namespace KwikNestaProperty.Domain.Entities
{
    public class ViewingRequest : BaseEntity
    {
        public Guid PropertyId { get; set; }
        public KNProperty Property { get; set; } = default!;
        public string UserId { get; set; } = default!;
        public DateTime RequestedDate { get; set; }
        public EViewingStatus Status { get; set; } = EViewingStatus.Pending;
    }
}