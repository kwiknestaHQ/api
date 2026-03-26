namespace KwikNesta.Shared.ServiceDTOs.Infra
{
    public class StateDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public Guid CountryId { get; set; }
        public string? CountryName { get; set; }
        public string CountryCode { get; set; } = default!;
        public string ISO2 { get; set; } = default!;
        public string Type { get; set; } = default!;
        public string Longitude { get; set; } = default!;
        public string Latitude { get; set; } = default!;
    }
}