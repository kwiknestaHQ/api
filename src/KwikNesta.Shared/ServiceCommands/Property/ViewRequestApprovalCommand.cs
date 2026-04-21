using KwikNesta.Mediator.Cores.Abstractions;
using KwikNesta.Shared.Responses;

namespace KwikNesta.Shared.ServiceCommands.Property
{
    public class ViewRequestApprovalCommand : IKNRequest<Response<string>>
    {
        public Guid RequestId { get; set; }
        public string? Token { get; set; }
        public string LoggedInUserId { get; set; } = default!;
        public string? IpAddress { get; set; }
    }
}