namespace KwikNesta.Shared.ServiceDTOs.Infra
{
    public class AgoraTokenDto
    {
        public string ChannelName { get; set; } = default!;
        public string Account { get; set; } = default!;
        public string Token { get; set; } = default!;
        public DateTime ExpiresAt { get; set; }
        public bool IsExpired => DateTime.UtcNow >= ExpiresAt;
    }
}
