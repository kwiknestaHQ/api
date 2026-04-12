using KwikNesta.Mediator.Cores.Abstractions;
using KwikNesta.Shared.Models;
using KwikNesta.Shared.Responses;
using KwikNesta.Shared.ServiceDTOs.Property;

namespace KwikNesta.Shared.ServiceQueries.Property
{
    public class GetPropertyDetailsQuery : IKNRequest<Response<PropertyDetailsDto>>
    {
        public Guid Id { get; set; }
        public UserContext Context { get; set; } = default!;
    }
}