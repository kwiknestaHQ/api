using KwikNesta.Mediator.Cores.Abstractions;
using KwikNesta.Shared.Responses;
using KwikNesta.Shared.ServiceDTOs.Property;

namespace KwikNesta.Shared.ServiceQueries.Property
{
    public class GetPropertyViewRequestsQuery : GetPropertyViewRequest, IKNRequest<PagedResponse<ViewRequestLeanDto>>
    {
        public string UserId { get; set; } = default!;
        public Guid PropertyId { get; set; }
    }

    public class GetPropertyViewRequest : BasePageQuery { }
}
