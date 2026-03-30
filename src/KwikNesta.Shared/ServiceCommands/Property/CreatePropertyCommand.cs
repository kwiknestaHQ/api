using KwikNesta.Mediator.Cores.Abstractions;
using KwikNesta.Shared.Models;
using KwikNesta.Shared.Responses;
using KwikNesta.Shared.ServiceDTOs.Property;

namespace KwikNesta.Shared.ServiceCommands.Property
{
    public class CreatePropertyCommand : CreatePropertyRequest, IKNRequest<Response<CreatePropertyResponseDto>>
    {
        public UserContext UserContext { get; set; } = default!;
    }

    public class CreatePropertyRequest : BasePropertyRequest
    {
        public CreateLocationDto Location { get; set; } = default!;
    }

    public class CreateLocationDto : BasePropertyLocationRequest { }
}