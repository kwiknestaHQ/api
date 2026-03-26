using KwikNesta.Shared.Models;
using KwikNesta.Shared.Models.Enumerations.Property;

namespace KwikNestaProperty.Domain.Entities
{
    public class PropertyFeature : BaseEntity
    {
        public string Name { get; set; } = default!;
        public string NameNormalized { get; set; } = default!;
        public EFeatureCategory Category { get; set; }
    }
}