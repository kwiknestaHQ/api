using KwikNesta.Mediator.Cores.Abstractions;
using KwikNesta.Shared.Responses;
using KwikNesta.Shared.ServiceDTOs.Property;

namespace KwikNesta.Shared.ServiceQueries.Property
{
    public class GetPropertyViewRequestByTokenQuery : IKNRequest<Response<ViewRequestDto>>
    {
        public string Token { get; set; } = default!;
        public Guid RequestId { get; set; }
    }
}