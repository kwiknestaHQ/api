using KwikNesta.Shared.Models.Enumerations.Infra;
using KwikNesta.Shared.ServiceDTOs.Infra;
using KwikNestaInfra.Infrastructure.Contracts;

namespace KwikNestaInfra.Infrastructure.Services.AgoraHandlers.Inspection
{
    public class AudienceJoinsHandler : IAgoraModuleHandler
    {
        public EAgoraModule Module => EAgoraModule.Inspection;

        public EAgoraEvent Event => EAgoraEvent.AudienceJoins;

        public Task HandleAsync(string noticeId, string entityId, AgoraPayloadBase payload)
        {
            throw new NotImplementedException();
        }
    }
}