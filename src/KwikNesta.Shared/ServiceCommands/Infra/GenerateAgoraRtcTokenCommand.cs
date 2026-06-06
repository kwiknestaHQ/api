using KwikNesta.Mediator.Cores.Abstractions;
using KwikNesta.Shared.Responses;
using KwikNesta.Shared.ServiceDTOs.Infra;

namespace KwikNesta.Shared.ServiceCommands.Infra
{
    public class GenerateAgoraRtcTokenCommand : IKNRequest<Response<AgoraRtcTokenResult>>
    {
        public string ChannelName { get; set; } = default!;
        public uint UId { get; set; }
        public bool IsPublisher { get; set; }
        public DateTime SessionScheduledTime { get; set; }
    }
}