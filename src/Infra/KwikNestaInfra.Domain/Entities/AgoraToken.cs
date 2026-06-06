using KwikNesta.Shared.Models;

namespace KwikNestaInfra.Domain.Entities
{
    public class AgoraToken : BaseEntity
    {
        public string ChannelName { get; set; } = default!;
        public string Account { get; set; } = default!;
        public string Token { get; set; } = default!;
        public DateTime ExpiresAt { get; set; }
    }
}
