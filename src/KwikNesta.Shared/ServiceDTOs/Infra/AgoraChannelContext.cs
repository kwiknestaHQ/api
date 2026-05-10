using KwikNesta.Shared.Models.Enumerations.Infra;

namespace KwikNesta.Shared.ServiceDTOs.Infra
{
    public class AgoraChannelContext
    {
        public string Environment { get; set; } = default!;
        public EAgoraModule Module { get; set; }
        public string EntityId { get; set; } = default!;
    }
}