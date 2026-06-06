using KwikNesta.Mediator.Cores.Abstractions;
using KwikNesta.Shared.Models.Enumerations.Property;
using KwikNesta.Shared.Responses;

namespace KwikNesta.Shared.ServiceCommands.Property
{
    public class ViewSessionUserJoinedCommand : IKNRequest<Response<string>>
    {
        public string ChannelName { get; set; } = default!;
        public uint UId { get; set; }
        public ESessionParticipantRole Role { get; set; }
        public DateTime Timestamp { get; set; }
    }
}