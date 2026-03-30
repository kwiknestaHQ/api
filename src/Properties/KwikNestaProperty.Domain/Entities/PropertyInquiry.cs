using KwikNesta.Shared.Models;

namespace KwikNestaProperty.Domain.Entities
{
    public class PropertyInquiry : BaseEntity
    {
        public Guid PropertyId { get; set; }
        public KNProperty Property { get; set; } = default!;
        public string UserId { get; set; } = default!;
        public decimal OfferedPrice { get; set; }
    }
}
