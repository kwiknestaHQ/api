using KwikNesta.Mediator.Cores.Abstractions;
using KwikNesta.Shared.Extensions;
using KwikNesta.Shared.Helpers;
using KwikNesta.Shared.Implementations;
using KwikNesta.Shared.Models.Enumerations.Identity;
using KwikNesta.Shared.Models.Settings;
using KwikNesta.Shared.Responses;
using KwikNesta.Shared.ServiceCommands.Identity;
using KwikNesta.Shared.ServiceDTOs.Identity;
using KwikNestaIdentity.Application.Validations;
using KwikNestaIdentity.Domain.Entities;
using KwikNestaIdentity.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace KwikNestaIdentity.Application.Handlers
{
    public class RegistrationCommandHandler : IKNRequestHandler<RegistrationCommand, Response<RegistrationDto>>
    {
        private readonly List<ESystemRoles> _accpetedRoles = new List<ESystemRoles> { ESystemRoles.LandLord, ESystemRoles.Tenant };
        private const int OtpExpirationMinute = 10;
        private readonly IIdentityRepositoryManager _repository;
        private readonly UserManager<User> _userManager;
        private readonly IHostEnvironment _host;
        private readonly JwtSettings _jwtSettings;

        public RegistrationCommandHandler(IIdentityRepositoryManager repository,
                                        UserManager<User> userManager,
                                        IOptions<KNApplicationSettings> options,
                                        IHostEnvironment host)
        {
            _repository = repository;
            _userManager = userManager;
            _host = host;
            _jwtSettings = options.Value.Jwt;
        }

        public async Task<Response<RegistrationDto>> HandleAsync(RegistrationCommand request, CancellationToken cancellationToken)
        {
            var validate = new RegistrationCommandValidator().Validate(request);
            if (!validate.IsValid)
            {
                return Response<RegistrationDto>.Fail(validate.Errors.FirstOrDefault()?.ErrorMessage ??
                    IdentityResponse.RegistrationFailed, 400);
            }

            if (!_accpetedRoles.Contains(request.Role))
            {
                return Response<RegistrationDto>.Fail(IdentityResponse.InvalidRegistrationRole, 401);
            }

            var existingUser = await _userManager.FindByEmailAsync(request.Email);
            if (existingUser != null)
            {
                return Response<RegistrationDto>.Fail(IdentityResponse.UserExists, 409);
            }

            var user = ObjectFactory.InitializeUser(request);
            var createResult = await _userManager.CreateAsync(user, request.Password);
            if (!createResult.Succeeded)
            {
                return Response<RegistrationDto>.Fail(createResult.Errors?.FirstOrDefault()?.Description ?? 
                    IdentityResponse.RegistrationFailed, 400);
            }

            var roleResult = await _userManager.AddToRoleAsync(user, request.Role.GetDescription());
            if (!roleResult.Succeeded)
            {
                await _userManager.DeleteAsync(user);
                return Response<RegistrationDto>.Fail(roleResult.Errors.FirstOrDefault()?.Description ??
                    IdentityResponse.RegistrationFailed, 400);
            }

            var otp = TokenHelper.GenerateOtp(8);
            var otpHash = TokenHelper.HashToken(otp, _jwtSettings.Key);
            var otpEntry = ObjectFactory.InitializeOtp(user.Id,
                    otpHash,
                    EOtpType.AccountVerification,
                    expirationMinutes: OtpExpirationMinute);

            await _repository.OtpEntry.AddAsync(otpEntry);
            await _repository.SaveAsync();

            Notifications.SendEmail(user.Email!, IdentityResponse.AccountActivationSubject, 
                _host.GetOtpNotification(user.FirstName, 
                                        IdentityResponse.AccountActivationMessage, 
                                        otp, 
                                        IdentityResponse.AccountActivationSecurityNotice, 
                                        OtpExpirationMinute));
            return Response<RegistrationDto>.Ok(new RegistrationDto(user.Email!));
        }
    }
}