using KwikNesta.Shared.Models.Enumerations.Infra;
using KwikNesta.Shared.ServiceDTOs.Infra;
using KwikNestaInfra.Infrastructure.Contracts;

namespace KwikNestaInfra.Infrastructure.Services.AgoraHandlers
{
    public abstract class AgoraModuleHandler<T> : IAgoraModuleHandler<T> where T : AgoraPayloadBase
    {
        public abstract EAgoraModule Module { get; }
        public abstract EAgoraEvent Event { get; }

        public Task HandleAsync(string noticeId, string entityId, AgoraPayloadBase payload)
        {
            if (payload is not T typed)
                throw new InvalidOperationException(
                    $"Expected {typeof(T).Name} but got {payload.GetType().Name}");

            return HandleAsync(noticeId, entityId, typed);
        }

        public abstract Task HandleAsync(string noticeId, string entityId, T payload);
    }
}