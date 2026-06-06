using KwikNesta.Mediator.Cores.Abstractions;
using KwikNesta.Shared.Responses;
using KwikNesta.Shared.ServiceCommands.Identity;
using KwikNestaIdentity.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace KwikNestaIdentity.Application.Handlers
{
    public class RecordNoShowCommandHandler(UserManager<User> userManager) : IKNRequestHandler<RecordNoShowCommand, Response<string>>
    {
        private readonly UserManager<User> _userManager = userManager;

        public async Task<Response<string>> HandleAsync(RecordNoShowCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.UserId))
            {
                return Response<string>.Fail(IdentityResponse.AccessDenied, 403);
            }

            var user = await _userManager.FindByIdAsync(request.UserId);
            if (user == null)
            {
                return Response<string>.Fail(IdentityResponse.UserNotFoundWithId, 404);
            }

            user.NoShowCount++;
            user.LastNoShowAt = DateTime.UtcNow;
            user.LastUpdatedOn = DateTime.UtcNow;
            await _userManager.UpdateAsync(user);

            return Response<string>.Ok(IdentityResponse.NoShowRecordedSuccessfully);
        }
    }
}