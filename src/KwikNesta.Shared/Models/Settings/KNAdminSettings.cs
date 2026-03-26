namespace KwikNesta.Shared.Models.Settings
{
    public class KNAdminSettings
    {
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;
        public string Phone { get; set; } = default!;
        public string BaseUrl { get; set; } = default!;
        public string SupportEmail { get; set; } = default!;
    }
}