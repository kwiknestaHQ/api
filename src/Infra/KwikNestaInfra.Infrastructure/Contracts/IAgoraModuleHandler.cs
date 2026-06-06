using KwikNesta.Shared.Models.Enumerations.Infra;
using KwikNesta.Shared.ServiceDTOs.Infra;

namespace KwikNestaInfra.Infrastructure.Contracts
{
    public interface IAgoraModuleHandler
    {
        EAgoraModule Module { get; }
        EAgoraEvent Event { get; }
        Task HandleAsync(string noticeId, string entityId, AgoraPayloadBase payload);
    }

    public interface IAgoraModuleHandler<T> : IAgoraModuleHandler where T : AgoraPayloadBase
    {
        Task HandleAsync(string noticeId, string entityId, T payload);
    }
}