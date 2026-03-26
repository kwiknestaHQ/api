using KwikNesta.Mediator.Cores.Abstractions;
using KwikNesta.Shared.Responses;
using KwikNesta.Shared.ServiceDTOs.Identity;

namespace KwikNesta.Shared.ServiceQueries.Identity
{
    public class GetUserByIdQuery : IKNRequest<Response<CurrentUserDto>>
    {
        public string Id { get; set; } = default!;
    }
}