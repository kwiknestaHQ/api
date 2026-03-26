using KwikNesta.Mediator.Cores.Abstractions;
using KwikNesta.Shared.Helpers;
using KwikNesta.Shared.Models.Enumerations.Identity;
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
    public class RefreshTokenCommandHandler : IKNRequestHandler<RefreshTokenCommand, Response<LoginResponseDto>>
    {
        private readonly UserManager<User> _userManager;
        private readonly IIdentityRepositoryManager _repository;
        private readonly JwtSettings _jwtSettings;

        public RefreshTokenCommandHandler(UserManager<User> userManager,
                                        IIdentityRepositoryManager repository,
                                        IOptions<KNApplicationSettings> options)
        {
            _userManager = userManager;
            _repository = repository;
            _jwtSettings = options.Value.Jwt;
        }

        public async Task<Response<LoginResponseDto>> HandleAsync(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.RefreshToken))
            {
                return Response<LoginResponseDto>.Fail(IdentityResponse.InvalidRefreshToken, 400);
            }

            var storedToken = await ValidateRefreshTokenAsync(request.RefreshToken);
            if (storedToken == null)
            {
                return Response<LoginResponseDto>.Fail(IdentityResponse.ExpiredToken, 403);
            }

            var user = await _userManager.FindByIdAsync(storedToken.UserId);
            if (user == null)
            {
                return Response<LoginResponseDto>.Fail(IdentityResponse.UserNotFoundWithId, 404);
            }

            if (user.Status != EUserStatus.Active)
            {
                return Response<LoginResponseDto>.Fail(IdentityResponse.UserInactive, 403);
            }

            var roles = await _userManager.GetRolesAsync(user);
            var newAccessToken = TokenHelper.CreateAccessToken(user.Id, user.Email!, [.. roles], _jwtSettings);
            return Response<LoginResponseDto>.Ok(new LoginResponseDto(newAccessToken, request.RefreshToken));
        }

        #region Private Methods
        private async Task<RefreshToken?> ValidateRefreshTokenAsync(string token)
        {
            var hash = TokenHelper.HashToken(token, _jwtSettings.Key);
            var refereshToken = await _repository.RefreshToken
                .FirstOrDefault(r => r.TokenHash == hash && !r.IsDeprecated, true);

            if (refereshToken == null || refereshToken.RevokedAt.HasValue)
            {
                return null;
            }

            if (refereshToken.ExpiresAt < DateTimeOffset.UtcNow)
            {
                refereshToken.RevokedAt = DateTimeOffset.UtcNow;
                refereshToken.IsDeprecated = true;
                await _repository.SaveAsync();

                return null;
            }

            return refereshToken;
        }
        #endregion
    }
}