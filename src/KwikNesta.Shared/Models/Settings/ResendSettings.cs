namespace KwikNesta.Shared.Models.Settings
{
    public class ResendSettings
    {
        public string ApiKey { get; set; } = default!;
        public string Sender { get; set; } = default!;
        public string BaseUrl { get; set; } = default!;
    }
}