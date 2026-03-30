using KwikNesta.Mediator.Cores.Abstractions;
using KwikNesta.Shared.Models.Enumerations.Property;
using KwikNesta.Shared.Responses;
using KwikNesta.Shared.ServiceDTOs.Property;

namespace KwikNesta.Shared.ServiceQueries.Property
{
    public class GetVerificationRequestsQuery : BasePageQuery, IKNRequest<PagedResponse<VerificationRequestLeanDto>>
    {
        public EVerificationStatus Status { get; set; } = EVerificationStatus.Pending;
    }
}