using KwikNesta.Mediator.Cores.Abstractions;
using KwikNesta.Shared.Models.Enumerations.Infra;
using KwikNesta.Shared.Responses;
using KwikNesta.Shared.ServiceDTOs.Infra;

namespace KwikNesta.Shared.ServiceQueries.Infra
{
    public class GetAgoraWebhookEventLogQuery : IKNRequest<Response<List<AgoraEventLogDto>>>
    {
        public string Channel { get; set; } = default!;
        public EAgoraEvent? Event { get; set; }
        public EAgoraModule? Module { get; set; }
    }
}