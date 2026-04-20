using KwikNesta.Mediator.Cores.Abstractions;
using KwikNesta.Shared.Models;
using KwikNesta.Shared.Models.Enumerations.Property;
using KwikNesta.Shared.Responses;
using KwikNesta.Shared.ServiceDTOs.Property;

namespace KwikNesta.Shared.ServiceCommands.Property
{
    public class CreateViewingRequestCommand : CreateViewingRequestDto, IKNRequest<Response<ViewingRequestResult>>
    {
        public Guid PropertyId { get; set; }
        public UserContext Context { get; set; } = default!;
    }

    public class CreateViewingRequestDto
    {
        public DateTime ScheduledDate { get; set; }
        public EViewingType Type { get; set; }
        public string? Note { get; set; }
    }
}