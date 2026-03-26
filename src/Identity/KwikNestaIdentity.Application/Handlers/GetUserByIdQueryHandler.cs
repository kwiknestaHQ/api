using KwikNesta.Mediator.Cores.Abstractions;
using KwikNesta.Shared.Responses;
using KwikNesta.Shared.ServiceDTOs.Identity;
using KwikNesta.Shared.ServiceQueries.Identity;
using KwikNestaIdentity.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace KwikNestaIdentity.Application.Handlers
{
    public class GetUserByIdQueryHandler(UserManager<User> userManager) 
        : IKNRequestHandler<GetUserByIdQuery, Response<CurrentUserDto>>
    {
        private readonly UserManager<User> _userManager = userManager;

        public async Task<Response<CurrentUserDto>> HandleAsync(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.Id);
            if (user == null)
            {
                return Response<CurrentUserDto>.Fail(IdentityResponse.UserNotFoundWithId, 404);
            }

            return Response<CurrentUserDto>.Ok(ObjectFactory.Map(user));
        }
    }
}