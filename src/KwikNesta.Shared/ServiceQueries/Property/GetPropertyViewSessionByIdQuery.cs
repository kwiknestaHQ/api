using KwikNesta.Mediator.Cores.Abstractions;
using KwikNesta.Shared.Responses;
using KwikNesta.Shared.ServiceDTOs.Property;

namespace KwikNesta.Shared.ServiceQueries.Property
{
    public class GetPropertyViewSessionByIdQuery : IKNRequest<Response<ViewRequestSessionDetailsDto>>
    {
        public Guid SessionId { get; set; }
    }
}