using KwikNesta.Shared.Models;

namespace KwikNestaProperty.Domain.Entities
{
    public class PropertyFeatureLink : BaseEntity
    {
        public Guid PropertyId { get; set; }
        public KNProperty Property { get; set; } = default!;

        public Guid? FeatureId { get; set; }
        public PropertyFeature? Feature { get; set; }

        public string? CustomFeature { get; set; }
        public string? CustomFeatureNormalized { get; set; }
    }
}
