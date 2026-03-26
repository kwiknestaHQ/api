using KwikNesta.Shared.Models;

namespace KwikNestaInfra.Domain.Entities
{
    public class KNCity : BaseEntity
    {
        public Guid StateId { get; set; }
        public KNState? State { get; set; }

        public Guid CountryId { get; set; }
        public KNCountry? Country { get; set; }

        public string Name { get; set; } = default!;
        public string Longitude { get; set; } = default!;
        public string Latitude { get; set; } = default!;
    }
}