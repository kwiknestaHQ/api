using KwikNesta.Mediator.Cores.Abstractions;
using KwikNesta.Shared.Responses;
using KwikNesta.Shared.ServiceDTOs.Identity;
using KwikNesta.Shared.ServiceQueries.Identity;
using KwikNestaIdentity.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace KwikNestaIdentity.Application.Handlers
{
    public class GetPagedUsersQueryHandler(UserManager<User> userManager) 
        : IKNRequestHandler<GetPagedUsersQuery, PagedResponse<CurrentUserDto>>
    {
        private readonly UserManager<User> _userManager = userManager;

        public async Task<PagedResponse<CurrentUserDto>> HandleAsync(GetPagedUsersQuery request, CancellationToken cancellationToken)
        {
            var users = _userManager.Users;
            if (request.Gender.HasValue)
            {
                users = users.Where(u => u.Gender == request.Gender.Value);
            }
            if (request.Status.HasValue)
            {
                users = users.Where(u => u.Status == request.Status.Value);
            }

            if (!string.IsNullOrWhiteSpace(request.Search))
            {
                var search = request.Search.ToLower();
                users = users.Where(u => 
                                u.FirstName.ToLower().Contains(search) || 
                                u.LastName.ToLower().Contains(search) ||
                                (!string.IsNullOrEmpty(u.Email) && u.Email.Contains(search)) ||
                                (!string.IsNullOrEmpty(u.OtherName) && u.OtherName.Contains(search)));
            }

            var data = users
                .OrderByDescending(a => a.CreatedOn)
                .Select(ObjectFactory.Map)
                .Paginate(request.Page, request.PageSize);

            await Task.CompletedTask;
            return data;
        }
    }
}
