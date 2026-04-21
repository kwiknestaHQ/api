using KwikNesta.Mediator.Cores.Abstractions;
using KwikNesta.Shared.Responses;
using KwikNesta.Shared.ServiceDTOs.Property;

namespace KwikNesta.Shared.ServiceQueries.Property
{
    public class GetPropertyViewRequestQuery : IKNRequest<Response<ViewRequestDto>>
    {
        public Guid PropertyId { get; set; }
        public Guid RequestId { get; set; }
        public string UserId { get; set; } = default!;
    }
}