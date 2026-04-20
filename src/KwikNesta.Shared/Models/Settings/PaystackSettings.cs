namespace KwikNesta.Shared.Models.Settings
{
    public class PaystackSettings
    {
        public string BaseUrl { get; set; } = default!;
        public string PublicKey { get; set; } = default!;
        public string PrivateKey { get; set; } = default!;
        public string CallbackUrl { get; set; } = default!;
    }
}