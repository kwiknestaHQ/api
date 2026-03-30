using KwikNesta.Mediator.Cores.Abstractions;
using KwikNesta.Shared.Models;
using KwikNesta.Shared.Responses;
using KwikNesta.Shared.ServiceDTOs.Property;

namespace KwikNesta.Shared.ServiceCommands.Property
{
    public class UpdatePropertyFeaturesCommand : UpdatePropertyFeaturesRequest, IKNRequest<Response<CreatePropertyResponseDto>>
    {
        public Guid PropertyId { get; set; }
        public UserContext UserContext { get; set; } = default!;
    }

    public class UpdatePropertyFeaturesRequest
    {

        public List<Guid> FeatureIds { get; set; } = new();
        public List<string> CustomFeatures { get; set; } = new();
    }
}