using KwikNesta.Shared.Models;

namespace KwikNestaProperty.Domain.Entities
{
    public class PropertyPriceHistory : BaseEntity
    {
        public Guid PropertyId { get; set; }
        public KNProperty Property { get; set; } = default!;

        public decimal OldPrice { get; set; }
        public decimal NewPrice { get; set; }
    }
}