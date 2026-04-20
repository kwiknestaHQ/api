using KwikNesta.Mediator.Cores.Abstractions;
using KwikNesta.Shared.Responses;
using KwikNesta.Shared.ServiceDTOs.Property;

namespace KwikNesta.Shared.ServiceQueries.Property
{
    public class GetViewRequestByIdQuery : IKNRequest<Response<ViewRequestDto>>
    {
        public Guid Id { get; set; }
    }
}