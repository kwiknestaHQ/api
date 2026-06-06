using KwikNesta.Mediator.Cores.Abstractions;
using KwikNesta.Shared.Responses;
using KwikNesta.Shared.ServiceDTOs.Infra;

namespace KwikNesta.Shared.ServiceQueries.Infra
{
    public class GetAgoraTokenQuery : IKNRequest<Response<AgoraTokenDto>>
    {
        public string Channel { get; set; } = default!;
        public string UserId { get; set; } = default!;
    }
}