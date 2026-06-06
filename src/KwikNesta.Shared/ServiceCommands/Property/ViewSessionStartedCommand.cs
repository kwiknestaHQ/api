using KwikNesta.Mediator.Cores.Abstractions;
using KwikNesta.Shared.Responses;

namespace KwikNesta.Shared.ServiceCommands.Property
{
    public class ViewSessionStartedCommand : IKNRequest<Response<string>>
    {
        public string ChannelName { get; set; } = default!;
        public DateTime Timestamp { get; set; }
    }
}