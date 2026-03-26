using KwikNesta.Mediator.Cores.Abstractions;
using KwikNesta.Shared.Extensions;
using KwikNesta.Shared.Implementations;
using KwikNesta.Shared.Models.Enumerations.Infra;
using KwikNesta.Shared.Models.Settings;
using KwikNesta.Shared.Responses;
using KwikNesta.Shared.ServiceCommands.Identity;
using KwikNestaIdentity.Application.Validations;
using KwikNestaIdentity.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace KwikNestaIdentity.Application.Handlers
{
    public class ChangePasswordCommandHandler(UserManager<User> userManager,
                                    IHostEnvironment host,
                                    IOptions<KNApplicationSettings> options) 
        : IKNRequestHandler<ChangePasswordCommand, Response<string>>
    {
        private readonly UserManager<User> _userManager = userManager;
        private readonly IHostEnvironment _host = host;
        private readonly string _supportEmail = options.Value.AppAdmin.SupportEmail;

        public async Task<Response<string>> HandleAsync(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var validator = new ChangePasswordCommandValidator().Validate(request);
            if (!validator.IsValid)
            {
                return Response<string>.Fail(validator.Errors.FirstOrDefault()?.ErrorMessage ?? 
                    IdentityResponse.InvalidRequest, 400);
            }

            var user = await _userManager.FindByIdAsync(request.UserId);
            if (user == null)
            {
                return Response<string>.Fail(IdentityResponse.AccessDenied, 403);
            }

            var result = await _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);
            if (!result.Succeeded)
            {
                return Response<string>.Fail($"{result.Errors.FirstOrDefault()?.Description}", 401);
            }

            Notifications.SendEmail(user.Email!, IdentityResponse.ChangePasswordInformationSubject,
                _host.GetInformationalNotification(user.FirstName,
                                        IdentityResponse.ChangePasswordInformationMessage,
                                        _supportEmail));

            AppAudit.Write(user.Id,
                        user.Email!,
                        EAuditAction.ChangedPassword,
                        EAuditDomain.UserAccount,
                        user.Id,
                        request.UserIpAddress);

            return Response<string>.Ok(IdentityResponse.PasswordChanged);
        }
    }
}