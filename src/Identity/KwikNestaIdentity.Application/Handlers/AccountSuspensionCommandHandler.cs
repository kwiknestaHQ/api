using KwikNesta.Mediator.Cores.Abstractions;
using KwikNesta.Shared.Extensions;
using KwikNesta.Shared.Implementations;
using KwikNesta.Shared.Models.Enumerations.Identity;
using KwikNesta.Shared.Models.Enumerations.Infra;
using KwikNesta.Shared.Models.Settings;
using KwikNesta.Shared.Responses;
using KwikNesta.Shared.ServiceCommands.Identity;
using KwikNestaIdentity.Application.Validations;
using KwikNestaIdentity.Domain.Entities;
using KwikNestaIdentity.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace KwikNestaIdentity.Application.Handlers
{
    public class AccountSuspensionCommandHandler(UserManager<User> userManager,
                                        IIdentityRepositoryManager repository,
                                        IHostEnvironment host,
                                        IOptions<KNApplicationSettings> options) 
        : IKNRequestHandler<AccountSuspensionCommand, Response<string>>
    {
        private readonly UserManager<User> _userManager = userManager;
        private readonly IIdentityRepositoryManager _repository = repository;
        private readonly IHostEnvironment _host = host;
        private readonly string _supportEmail = options.Value.AppAdmin.SupportEmail;

        public async Task<Response<string>> HandleAsync(AccountSuspensionCommand request, CancellationToken cancellationToken)
        {
            var validator = new AccountSuspensionCommandValidator().Validate(request);
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

            if (userToUpdate.Status == EUserStatus.Suspended)
            {
                return Response<string>.Fail(IdentityResponse.UserAlreadySuspended, 409);
            }

            userToUpdate.Status = EUserStatus.Suspended;
            userToUpdate.LastUpdatedOn = DateTime.UtcNow;
            userToUpdate.StatusChangedAt = DateTime.UtcNow;
            await _userManager.UpdateAsync(userToUpdate);

            var tokens = await _repository.RefreshToken
                .Get(rt => rt.UserId.Equals(userToUpdate.Id))
                .ToListAsync(cancellationToken);
            if (tokens.Count != 0)
            {
                _repository.RefreshToken.RemoveMany(tokens);
                await _repository.SaveAsync();
            }

            var message = string.Format(IdentityResponse.SuspensionInformationMessage, 
                string.IsNullOrWhiteSpace(request.OtherReason) ? request.Reason.GetDescription() : request.OtherReason);
            Notifications.SendEmail(userToUpdate.Email!, IdentityResponse.SuspensionInformationSubject,
                _host.GetInformationalNotification(userToUpdate.FirstName,
                                        message,
                                        _supportEmail));

            AppAudit.Write(loggedInUser.Id,
                        loggedInUser.Email!,
                        EAuditAction.SuspendedAccount,
                        EAuditDomain.UserAccount,
                        userToUpdate.Id,
                        request.LoggedInUserIpAddress,
                        request.OtherReason);
            return Response<string>.Ok(IdentityResponse.AccountSuspended);
        }
    }
}
