using KwikNesta.Mediator.Cores.Abstractions;
using KwikNesta.Shared.Responses;
using KwikNesta.Shared.ServiceDTOs.Property;

namespace KwikNesta.Shared.ServiceQueries.Property
{
    public class GetPropertyLeanQuery : IKNRequest<Response<PropertyVeryLeanDto>>
    {
        public Guid Id { get; set; }
    }
}