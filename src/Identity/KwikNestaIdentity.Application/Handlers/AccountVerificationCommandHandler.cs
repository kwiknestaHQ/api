using KwikNesta.Mediator.Cores.Abstractions;
using KwikNesta.Shared.Helpers;
using KwikNesta.Shared.Implementations;
using KwikNesta.Shared.Models.Settings;
using KwikNesta.Shared.Responses;
using KwikNestaIdentity.Domain.Entities;
using KwikNestaIdentity.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using KwikNesta.Shared.Extensions;
using KwikNesta.Shared.Constants;
using KwikNesta.Shared.ServiceCommands.Identity;
using KwikNesta.Shared.Models.Enumerations.Identity;
using Microsoft.EntityFrameworkCore;

namespace KwikNestaIdentity.Application.Handlers
{
    public class AccountVerificationCommandHandler : IKNRequestHandler<AccountVerificationCommand, Response<string>>
    {
        private readonly UserManager<User> _userManager;
        private readonly IIdentityRepositoryManager _repository;
        private readonly JwtSettings _jwtSettings;
        private readonly IHostEnvironment _host;
        private readonly KNAdminSettings _adminSettings;
        private readonly int delayInHours = 1;

        public AccountVerificationCommandHandler(UserManager<User> userManager,
                                                IIdentityRepositoryManager repository,
                                                IOptions<KNApplicationSettings> options,
                                                IHostEnvironment host)
        {
            _userManager = userManager;
            _repository = repository;
            _jwtSettings = options.Value.Jwt;
            _host = host;
            _adminSettings = options.Value.AppAdmin;
        }

        public async Task<Response<string>> HandleAsync(AccountVerificationCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.Otp) || string.IsNullOrWhiteSpace(request.Email))
            {
                return Response<string>.Fail(IdentityResponse.InvalidRequest, 400);
            }

            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                return Response<string>.Fail(IdentityResponse.UserNotFoundWithEmail, 404);
            }

            var hash = TokenHelper.HashToken(request.Otp, _jwtSettings.Key);
            var otpEntry = await _repository.OtpEntry
                .Get(o => o.UserId.Equals(user.Id) &&
                                    o.Type == EOtpType.AccountVerification &&
                                    o.OtpHash.Equals(hash))
                .OrderByDescending(o => o.CreatedOn)
                .FirstOrDefaultAsync(cancellationToken);

            if (otpEntry == null)
            {
                return Response<string>.Fail(IdentityResponse.InvalidOtp, 404);
            }

            if (otpEntry.ExpiresAt < DateTime.UtcNow)
            {
                return Response<string>.Fail(IdentityResponse.OtpExpired, 403);
            }

            user.EmailConfirmed = true;
            user.LastUpdatedOn = DateTime.UtcNow;
            user.Status = EUserStatus.Active;
            await _userManager.UpdateAsync(user);
            
            _repository.OtpEntry.Remove(otpEntry);
            await _repository.SaveAsync();

            Notifications.SendScheduledEmail(user.Email!, 
                string.Format(IdentityResponse.WelcomeEmailSubject, AppConstants.Platform),
                _host.GetWelcomeNotification(user.FirstName, 
                                        _adminSettings.BaseUrl, 
                                        _adminSettings.SupportEmail),
                delayInHours);

            return Response<string>.Ok(IdentityResponse.AccountVerificationSuccessful);
        }
    }
}