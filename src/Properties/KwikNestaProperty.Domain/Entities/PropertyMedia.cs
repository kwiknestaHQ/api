using KwikNesta.Shared.Models;
using KwikNesta.Shared.Models.Enumerations.Property;

namespace KwikNestaProperty.Domain.Entities
{
    public class PropertyMedia : BaseEntity
    {
        public Guid PropertyId { get; set; }
        public KNProperty Property { get; set; } = default!;

        public string Url { get; set; } = default!;
        public bool IsPrimary { get; set; }
        public int Order { get; set; }
        public EMediaType Type { get; set; }
    }
}