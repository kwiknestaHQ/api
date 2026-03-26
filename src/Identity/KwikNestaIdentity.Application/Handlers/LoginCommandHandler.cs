using KwikNesta.Mediator.Cores.Abstractions;
using KwikNesta.Shared.Helpers;
using KwikNesta.Shared.Implementations;
using KwikNesta.Shared.Models.Enumerations.Identity;
using KwikNesta.Shared.Models.Enumerations.Infra;
using KwikNesta.Shared.Models.Settings;
using KwikNesta.Shared.Responses;
using KwikNesta.Shared.ServiceCommands.Identity;
using KwikNesta.Shared.ServiceDTOs.Identity;
using KwikNestaIdentity.Domain.Entities;
using KwikNestaIdentity.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace KwikNestaIdentity.Application.Handlers
{
    public class LoginCommandHandler : IKNRequestHandler<LoginCommand, Response<LoginResponseDto>>
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IIdentityRepositoryManager _repository;
        private readonly JwtSettings _jwtSettings;

        public LoginCommandHandler(UserManager<User> userManager,
                                SignInManager<User> signInManager,
                                IIdentityRepositoryManager repository,
                                IOptions<KNApplicationSettings> options)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _repository = repository;
            _jwtSettings = options.Value.Jwt;
        }

        public async Task<Response<LoginResponseDto>> HandleAsync(LoginCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await ValidateUser(request);
            if (!validationResult.Success)
            {
                return Response<LoginResponseDto>.Fail(validationResult.Message, validationResult.StatusCode);
            }

            var (user, roles) = validationResult.Data;
            var accessToken = TokenHelper.CreateAccessToken(user.Id, user.Email!, roles, _jwtSettings);
            var refreshToken = TokenHelper.GenerateRandomBase64String();

            user.LastUpdatedOn = DateTime.UtcNow;
            user.LastLogin = DateTime.UtcNow;
            await _userManager.UpdateAsync(user);

            var tokenHash = TokenHelper.HashToken(refreshToken, _jwtSettings.Key);
            await _repository.RefreshToken.AddAsync(new RefreshToken
            {
                TokenHash = tokenHash,
                UserId = user.Id,
                ExpiresAt = DateTimeOffset.UtcNow.AddHours(_jwtSettings.ExpirationMinutes)
            });

            await _repository.SaveAsync();

            AppAudit.Write(user.Id, 
                        user.Email!, 
                        EAuditAction.Login, 
                        EAuditDomain.UserAccount, 
                        user.Id, 
                        request.UserIpAddress);
            return Response<LoginResponseDto>.Ok(new LoginResponseDto(accessToken, refreshToken));
        }

        private async Task<Response<(User User, string[] Roles)>> ValidateUser(LoginCommand request)
        {
            if (string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Password))
            {
                return Response<(User User, string[] Roles)>.Fail(IdentityResponse.InvalidEmailOrPassword, 400);
            }

            var user = await _userManager.FindByNameAsync(request.Email);
            if (user == null)
            {
                return Response<(User User, string[] Roles)>.Fail(IdentityResponse.UserNotFoundWithEmail, 404);
            }

            if (!user.EmailConfirmed || user.Status != EUserStatus.Active)
            {
                return Response<(User User, string[] Roles)>.Fail(IdentityResponse.EmailNotConfirmedOrAccountInactive, 400);
            }

            var check = await _signInManager.CheckPasswordSignInAsync(user, request.Password, lockoutOnFailure: true);
            if (!check.Succeeded)
            {
                return Response<(User User, string[] Roles)>.Fail(IdentityResponse.WrongPassword, 400);
            }

            var roles = (await _userManager.GetRolesAsync(user)).ToArray();
            if (roles == null || roles.Length == 0)
            {
                return Response<(User User, string[] Roles)>.Fail(IdentityResponse.NoAssignedRoles, 400);
            }

            return Response<(User User, string[] Roles)>.Ok((user, roles));
        }
    }
}