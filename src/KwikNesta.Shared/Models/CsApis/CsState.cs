namespace KwikNesta.Shared.Models.CsApis
{
    public class CsState
    {
        public long Id { get; set; }
        public string Name { get; set; } = default!;
        public long Country_Id { get; set; }
        public string Country_Code { get; set; } = default!;
        public string ISO2 { get; set; } = default!;
        public string Type { get; set; } = default!;
        public string Longitude { get; set; } = default!;
        public string Latitude { get; set; } = default!;
    }
}