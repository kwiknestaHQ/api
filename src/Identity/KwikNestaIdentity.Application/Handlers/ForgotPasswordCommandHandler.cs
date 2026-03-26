using KwikNesta.Mediator.Cores.Abstractions;
using KwikNesta.Shared.Extensions;
using KwikNesta.Shared.Helpers;
using KwikNesta.Shared.Implementations;
using KwikNesta.Shared.Models.Enumerations.Identity;
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
    public class ForgotPasswordCommandHandler(UserManager<User> userManager,
                                    IIdentityRepositoryManager repository,
                                    IHostEnvironment host,
                                    IOptions<KNApplicationSettings> options) 
        : IKNRequestHandler<ForgotPasswordCommand, Response<string>>
    {
        private readonly UserManager<User> _userManager = userManager;
        private readonly IIdentityRepositoryManager _repository = repository;
        private readonly IHostEnvironment _host = host;
        private readonly JwtSettings _jwtSettings = options.Value.Jwt;
        private readonly string _supportEmail = options.Value.AppAdmin.SupportEmail;

        public async Task<Response<string>> HandleAsync(ForgotPasswordCommand request, CancellationToken cancellationToken)
        {
            var validator = new ForgotPasswordCommandValidator().Validate(request);
            if (!validator.IsValid)
            {
                return Response<string>.Fail(validator.Errors.FirstOrDefault()?.ErrorMessage ?? IdentityResponse.InvalidRequest, 400);
            }

            var user = await _userManager.FindByEmailAsync(request.Email);
            if(user == null)
            {
                return Response<string>.Fail(IdentityResponse.UserNotFoundWithEmail, 404);
            }

            var hash = TokenHelper.HashToken(request.Otp, _jwtSettings.Key);
            var otpEntry = await _repository.OtpEntry
                .Get(o => o.UserId.Equals(user.Id) &&
                                    o.Type == EOtpType.PasswordReset &&
                                    o.OtpHash.Equals(hash))
                .OrderByDescending(o => o.CreatedOn)
                .FirstOrDefaultAsync(cancellationToken);

            if (otpEntry == null || string.IsNullOrWhiteSpace(otpEntry.TokenHash))
            {
                return Response<string>.Fail(IdentityResponse.InvalidOtp, 404);
            }

            if (otpEntry.ExpiresAt < DateTime.UtcNow)
            {
                return Response<string>.Fail(IdentityResponse.OtpExpired, 403);
            }

            var token = TokenHelper.Decrypt(otpEntry.TokenHash, _jwtSettings.Key);
            var result = await _userManager
                .ResetPasswordAsync(user, Uri.UnescapeDataString(token), request.NewPassword);
            if (!result.Succeeded)
            {
                return Response<string>.Fail(result.Errors.FirstOrDefault()?.Description ?? 
                    IdentityResponse.PasswordChangedFailed, 403);
            }

            user.LastUpdatedOn = DateTime.UtcNow;
            await _userManager.UpdateAsync(user);
            _repository.OtpEntry.Remove(otpEntry);
            await _repository.SaveAsync();

            Notifications.SendEmail(user.Email!, IdentityResponse.ForgotPasswordInformationSubject,
                _host.GetInformationalNotification(user.FirstName,
                                        IdentityResponse.ForgotPasswordInformationMessage,
                                        _supportEmail));
            return Response<string>.Ok(IdentityResponse.PasswordChanged);
        }
    }
}
