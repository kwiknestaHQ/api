using KwikNesta.Shared.Models;

namespace KwikNestaProperty.Domain.Entities
{
    public class PropertyView : BaseEntity
    {
        public Guid PropertyId { get; set; }
        public KNProperty Property { get; set; } = default!;

        public string? UserId { get; set; }
        public string? IpAddress { get; set; }

        public DateTime ViewedAt { get; set; }
    }
}
