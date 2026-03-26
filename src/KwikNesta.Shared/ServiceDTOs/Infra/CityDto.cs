namespace KwikNesta.Shared.ServiceDTOs.Infra
{
    public class CityDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public string Longitude { get; set; } = default!;
        public string Latitude { get; set; } = default!;
        public Guid StateId { get; set; }
        public string? StateName { get; set; } = default!;

        public Guid CountryId { get; set; }
        public string? CountryName { get; set; } = default!;
    }
}
