namespace KwikNesta.Shared.ServiceCommands.Property
{
    public abstract class BasePropertyLocationRequest
    {
        public string Address { get; set; } = default!;
        public string City { get; set; } = default!;
        public Guid StateId { get; set; }
        public Guid CountryId { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
    }
}