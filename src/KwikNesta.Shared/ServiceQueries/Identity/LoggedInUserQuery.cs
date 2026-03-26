using KwikNesta.Mediator.Cores.Abstractions;
using KwikNesta.Shared.Responses;
using KwikNesta.Shared.ServiceDTOs.Identity;

namespace KwikNesta.Shared.ServiceQueries.Identity
{
    public class LoggedInUserQuery : IKNRequest<Response<CurrentUserDto>>
    {
        public string UserId { get; set; } = default!;
    }
}