using KwikNesta.Mediator.Cores.Abstractions;
using KwikNesta.Shared.Models.Enumerations.Identity;
using KwikNesta.Shared.Responses;
using KwikNesta.Shared.ServiceDTOs.Identity;

namespace KwikNesta.Shared.ServiceQueries.Identity
{
    public class GetPagedUsersQuery : BasePageQuery, IKNRequest<PagedResponse<CurrentUserDto>>
    {
        public string? Search { get; set; } = default!;
        public EGender? Gender { get; set; }
        public EUserStatus? Status { get; set; }
    }
}