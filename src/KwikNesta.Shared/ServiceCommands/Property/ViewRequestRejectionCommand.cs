using KwikNesta.Mediator.Cores.Abstractions;
using KwikNesta.Shared.Models;
using KwikNesta.Shared.Responses;

namespace KwikNesta.Shared.ServiceCommands.Property
{
    public class ViewRequestRejectionCommand : IKNRequest<Response<string>>
    {
        public UserContext Context { get; set; } = default!;
        public Guid RequestId { get; set; }
        public string? Token { get; set; }
    }
}