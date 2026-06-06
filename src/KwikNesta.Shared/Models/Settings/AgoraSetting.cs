namespace KwikNesta.Shared.Models.Settings
{
    public class AgoraSetting
    {
        public string AppId { get; set; } = default!;
        public string AppCert { get; set; } = default!;
        public string WebhookSecret { get; set; } = default!;
        public int ExpiryInSeconds { get; set; }
        public int MinAttendanceDurationSeconds { get; set; }
    }
}