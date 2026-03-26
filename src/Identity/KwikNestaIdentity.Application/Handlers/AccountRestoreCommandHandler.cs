using KwikNesta.Mediator.Cores.Abstractions;
using KwikNesta.Shared.Extensions;
using KwikNesta.Shared.Implementations;
using KwikNesta.Shared.Models.Enumerations.Identity;
using KwikNesta.Shared.Models.Enumerations.Infra;
using KwikNesta.Shared.Responses;
using KwikNesta.Shared.ServiceCommands.Identity;
using KwikNestaIdentity.Application.Validations;
using KwikNestaIdentity.Domain.Entities;
using KwikNestaIdentity.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Hosting;

namespace KwikNestaIdentity.Application.Handlers
{
    public class AccountRestoreCommandHandler(UserManager<User> userManager,
                                    IIdentityRepositoryManager repository,
                                    IHostEnvironment host) 
        : IKNRequestHandler<AccountRestoreCommand, Response<string>>
    {
        private readonly UserManager<User> _userManager = userManager;
        private readonly IIdentityRepositoryManager _repository = repository;
        private readonly IHostEnvironment _host = host;

        public async Task<Response<string>> HandleAsync(AccountRestoreCommand request, CancellationToken cancellationToken)
        {
            var validator = new AccountRestoreCommandValidator().Validate(request);
            if (!validator.IsValid)
            {
                return Response<string>.Fail(validator.Errors.FirstOrDefault()?.ErrorMessage ?? IdentityResponse.InvalidRequest, 400);
            }

            var loggedInUser = await _userManager.FindByIdAsync(request.LoggedInUserId);
            if (loggedInUser == null)
            {
                return Response<string>.Fail(IdentityResponse.AccessDenied, 403);
            }

            var roles = (await _userManager.GetRolesAsync(loggedInUser))?.ToList();
            if (roles == null || !roles.Contains(ESystemRoles.SuperAdmin.GetDescription()) &&
                !roles.Contains(ESystemRoles.Admin.GetDescription()))
            {
                return Response<string>.Fail(IdentityResponse.AccessDenied, 403);
            }

            var userToUpdate = await _userManager.FindByIdAsync(request.UserId);
            if (userToUpdate == null)
            {
                return Response<string>.Fail(IdentityResponse.UserNotFoundWithEmail, 404);
            }

            if (userToUpdate.Status != EUserStatus.Suspended)
            {
                return Response<string>.Fail(IdentityResponse.UserNotSuspended, 409);
            }

            userToUpdate.Status = EUserStatus.Active;
            userToUpdate.LastUpdatedOn = DateTime.UtcNow;
            userToUpdate.StatusChangedAt = DateTime.UtcNow;
            await _userManager.UpdateAsync(userToUpdate);

            Notifications.SendEmail(userToUpdate.Email!, IdentityResponse.AccountRestorationInformationSubject,
                _host.GetInformationalNotification(userToUpdate.FirstName,
                                        IdentityResponse.AccountRestorationInformationMessage));

            AppAudit.Write(loggedInUser.Id,
                        loggedInUser.Email!,
                        EAuditAction.RestoredAccount,
                        EAuditDomain.UserAccount,
                        userToUpdate.Id,
                        request.LoggedInUserIpAddress);
            return Response<string>.Ok(IdentityResponse.UserRestored);
        }
    }
}