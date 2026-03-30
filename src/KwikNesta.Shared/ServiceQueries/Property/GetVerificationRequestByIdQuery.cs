using KwikNesta.Mediator.Cores.Abstractions;
using KwikNesta.Shared.Responses;
using KwikNesta.Shared.ServiceDTOs.Property;

namespace KwikNesta.Shared.ServiceQueries.Property
{
    public class GetVerificationRequestByIdQuery : IKNRequest<Response<VerificationRequestDto>>
    {
        public Guid RequestId { get; set; }
    }
}