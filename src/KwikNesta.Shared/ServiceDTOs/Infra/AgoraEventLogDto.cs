using KwikNesta.Shared.Models.Enumerations.Infra;

namespace KwikNesta.Shared.ServiceDTOs.Infra
{
    public class AgoraEventLogDto
    {
        public string Channel { get; set; } = default!;
        public EAgoraEvent Type { get; set; }
        public EAgoraModule Module { get; set; }
        public DateTime Timestamp { get; set; }
        public uint? UId { get; set; }
    }
}