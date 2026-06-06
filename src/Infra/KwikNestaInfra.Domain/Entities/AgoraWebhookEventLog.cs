using KwikNesta.Shared.Models.Enumerations.Infra;

namespace KwikNestaInfra.Domain.Entities
{
    public class AgoraWebhookEventLog
    {
        public string Id { get; set; } = default!;
        public string Channel { get; set; } = default!;
        public EAgoraEvent Type { get; set; }
        public EAgoraModule Module { get; set; }
        public DateTime Timestamp { get; set; }
        public uint? UId { get; set; }
        public EAgoraPlatform Platform { get; set; }
        public long? Duration { get; set; }
        public int? Reason { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}