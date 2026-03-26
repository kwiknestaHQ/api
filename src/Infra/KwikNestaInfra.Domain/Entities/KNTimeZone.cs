using KwikNesta.Shared.Models;

namespace KwikNestaInfra.Domain.Entities
{
    public class KNTimeZone : BaseEntity
    {
        public Guid CountryId { get; set; }
        public KNCountry? Country { get; set; }
        public string ZoneName { get; set; } = default!;
        public int GMTOffset { get; set; }
        public string GMTOffsetName { get; set; } = default!;
        public string Abbreviation { get; set; } = default!;
        public string TZName { get; set; } = default!;
    }
}