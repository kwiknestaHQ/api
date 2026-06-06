namespace KwikNesta.Shared.ServiceDTOs.Property
{
    public class AgoraSessionTokenDto
    {
        public string AppId { get; set; } = default!;
        public string Channel { get; set; } = default!;
        public string Token { get; set; } = default!;
        public uint UId { get; set; }
    }
}