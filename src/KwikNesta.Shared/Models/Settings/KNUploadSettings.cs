namespace KwikNesta.Shared.Models.Settings
{
    public class KNUploadSettings
    {
        public string AccessKeyId { get; set; } = default!;
        public string SecretAccessKey { get; set; } = default!;
        public string Endpoint { get; set; } = default!;
        public string BucketName { get; set; } = default!;
        public string CdnUrl { get; set; } = default!;
    }
}