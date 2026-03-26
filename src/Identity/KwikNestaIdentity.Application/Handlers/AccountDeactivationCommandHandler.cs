using KwikNesta.Mediator.Cores.Abstractions;
using KwikNesta.Shared.Extensions;
using KwikNesta.Shared.Implementations;
using KwikNesta.Shared.Models.Enumerations.Identity;
using KwikNesta.Shared.Models.Enumerations.Infra;
using KwikNesta.Shared.Models.Settings;
using KwikNesta.Shared.Responses;
using KwikNesta.Shared.ServiceCommands.Identity;
using KwikNestaIdentity.Domain.Entities;
using KwikNestaIdentity.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace KwikNestaIdentity.Application.Handlers
{
    public class AccountDeactivationCommandHandler : IKNRequestHandler<AccountDeactivationCommand, Response<string>>
    {
        private readonly UserManager<User> _userManager;
        private readonly IIdentityRepositoryManager _repository;
        private readonly IHostEnvironment _host;
        private readonly string _supportEmail;

        public AccountDeactivationCommandHandler(UserManager<User> userManager,
                                            IIdentityRepositoryManager repository,
                                            IHostEnvironment host,
                                            IOptions<KNApplicationSettings> options)
        {
            _userManager = userManager;
            _repository = repository;
            _host = host;
            _supportEmail = options.Value.AppAdmin.SupportEmail;
        }

        public async Task<Response<string>> HandleAsync(AccountDeactivationCommand request, CancellationToken cancellationToken)
        {

            if (string.IsNullOrWhiteSpace(request.LoggedInUserId) || !request.Self)
            {
                return Response<string>.Fail(IdentityResponse.AccessDenied, 403);
            }

            var user = await _userManager.FindByIdAsync(request.UserId);
            if (user == null)
            {
                return Response<string>.Fail(IdentityResponse.UserNotFoundWithId, 404);
            }

            user.Status = EUserStatus.Deactivated;
            user.LastUpdatedOn = DateTime.UtcNow;
            user.StatusChangedAt = DateTime.UtcNow;
            await _userManager.UpdateAsync(user);

            var tokens = await _repository.RefreshToken
                .Get(rt => rt.UserId.Equals(user.Id))
                .ToListAsync(cancellationToken);
            if (tokens.Count != 0)
            {
                _repository.RefreshToken.RemoveMany(tokens);
                await _repository.SaveAsync();
            }

            AppAudit.Write(request.LoggedInUserId,
                        request.LoggedInUserId,
                        EAuditAction.DeactivatedAccount,
                        EAuditDomain.UserAccount,
                        user.Id,
                        request.UserIpAddress);

            Notifications.SendEmail(user.Email!, IdentityResponse.AccountDeactivationSubject,
               _host.GetInformationalNotification(user.FirstName,
                                       IdentityResponse.AccountDeactivationMessage,
                                       _supportEmail));

            return Response<string>.Ok(IdentityResponse.AccountDeactivationSuccessful);
        }
    }
}