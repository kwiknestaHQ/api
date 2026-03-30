using KwikNesta.Mediator.Cores.Abstractions;
using KwikNesta.Shared.Models;
using KwikNesta.Shared.Models.Enumerations.Property;
using KwikNesta.Shared.Responses;

namespace KwikNesta.Shared.ServiceCommands.Property
{
    public class ReviewVerificationCommand : ReviewVerificationRequest, IKNRequest<Response<string>>
    {
        public Guid PropertyId { get; set; }
        public Guid RequestId { get; set; }
        public UserContext UserContext { get; set; } = default!;
    }

    public class ReviewVerificationRequest
    {
        public EVerificationAction Action { get; set; }
        public string? Reason { get; set; }
    }
}