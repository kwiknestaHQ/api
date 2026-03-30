using System.Text.Json.Serialization;

namespace KwikNesta.Shared.Models
{
    public class ReverseGeocodeResult
    {
        [JsonPropertyName("lat")]
        public string Latitude { get; set; } = default!;
        [JsonPropertyName("lon")]
        public string Longitude { get; set; } = default!;
        [JsonPropertyName("display_name")]
        public string DisplayName { get; set; } = default!;
        public GeoCodeAddress Address { get; set; } = default!;
    }

    public class GeoCodeAddress
    {
        public string Road { get; set; } = default!;
        public string Neighbourhood { get; set; } = default!;
        public string PostCode { get; set; } = default!;
        public string City { get; set; } = default!;
        public string State { get; set; } = default!;
        public string Country { get; set; } = default!;
    }
}
