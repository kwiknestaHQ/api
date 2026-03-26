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
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace KwikNestaIdentity.Application.Handlers
{
    public class ResetPasswordCommandHandler : IKNRequestHandler<ResetPasswordCommand, Response<string>>
    {
        private readonly UserManager<User> _userManager;
        private readonly IIdentityRepositoryManager _repository;
        private readonly IHostEnvironment _host;
        private readonly JwtSettings _jwtSettings;
        private const int OtpExpirationMinute = 10;

        public ResetPasswordCommandHandler(UserManager<User> userManager,
                                        IIdentityRepositoryManager repository,
                                        IHostEnvironment host,
                                        IOptions<KNApplicationSettings> options)
        {
            _userManager = userManager;
            _repository = repository;
            _host = host;
            _jwtSettings = options.Value.Jwt;
        }

        public async Task<Response<string>> HandleAsync(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var validator = new ResetPasswordCommandValidator().Validate(request);
            if (!validator.IsValid)
            {
                return Response<string>.Fail(validator.Errors.FirstOrDefault()?.ErrorMessage ?? IdentityResponse.InvalidRequest, 400);
            }

            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                return Response<string>.Fail(IdentityResponse.UserNotFoundWithEmail, 404);
            }

            var otp = TokenHelper.GenerateOtp(8);
            var otpHash = TokenHelper.HashToken(otp, _jwtSettings.Key);
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var tokenHash = TokenHelper.Encrypt(token, _jwtSettings.Key);
            var otpEntry = ObjectFactory.InitializeOtp(user.Id,
                    otpHash,
                    EOtpType.PasswordReset,
                    tokenHash,
                    OtpExpirationMinute);

            await _repository.OtpEntry.AddAsync(otpEntry);
            await _repository.SaveAsync();

            Notifications.SendEmail(user.Email!, IdentityResponse.PasswordResetSubject,
                _host.GetOtpNotification(user.FirstName,
                                        IdentityResponse.PasswordResetMessage,
                                        otp,
                                        IdentityResponse.PasswordResetSecurityNotice,
                                        OtpExpirationMinute));

            return Response<string>.Ok(IdentityResponse.PasswordResetSuccessful);
        }
    }
}