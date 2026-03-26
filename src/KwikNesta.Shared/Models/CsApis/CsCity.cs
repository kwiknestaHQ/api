namespace KwikNesta.Shared.Models.CsApis
{
    public class CsCity
    {
        public long Id { get; set; }
        public string Name { get; set; } = default!;
        public string Longitude { get; set; } = default!;
        public string Latitude { get; set; } = default!;
    }
}