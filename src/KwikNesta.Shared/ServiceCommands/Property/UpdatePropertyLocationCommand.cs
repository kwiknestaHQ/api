using KwikNesta.Mediator.Cores.Abstractions;
using KwikNesta.Shared.Models;
using KwikNesta.Shared.Responses;

namespace KwikNesta.Shared.ServiceCommands.Property
{
    public class UpdatePropertyLocationCommand : UpdatePropertyLocationRequest, IKNRequest<Response<string>>
    {
        public Guid PropertyId { get; set; }
        public UserContext UserContext { get; set; } = default!;
    }

    public class UpdatePropertyLocationRequest : BasePropertyLocationRequest { }
}