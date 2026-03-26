using KwikNesta.Mediator.Cores.Abstractions;
using KwikNesta.Shared.Implementations;
using KwikNesta.Shared.Models.Enumerations.Infra;
using KwikNesta.Shared.Responses;
using KwikNesta.Shared.ServiceCommands.Identity;
using KwikNestaIdentity.Application.Validations;
using KwikNestaIdentity.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace KwikNestaIdentity.Application.Handlers
{
    public class BasicUserDetailsUpdateCommandHandler(UserManager<User> userManager) 
        : IKNRequestHandler<BasicUserDetailsUpdateCommand, Response<string>>
    {
        private readonly UserManager<User> _userManager = userManager;

        public async Task<Response<string>> HandleAsync(BasicUserDetailsUpdateCommand request, CancellationToken cancellationToken)
        {
            var validate = new BasicUserDetailsUpdateCommandValidator().Validate(request);
            if (!validate.IsValid)
            {
                return Response<string>.Fail(validate.Errors.FirstOrDefault()?.ErrorMessage ??
                    IdentityResponse.InvalidRequest, 400);
            }

            var user = await _userManager.FindByIdAsync(request.LoggedInUserId);
            if (user == null)
            {
                return Response<string>.Fail(IdentityResponse.UserNotFoundWithEmail, 404);
            }

            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.OtherName = request.OtherName;
            user.Gender = request.Gender;
            user.LastUpdatedOn = DateTime.UtcNow;
            await _userManager.UpdateAsync(user);

            AppAudit.Write(request.LoggedInUserId,
                        user.Email!,
                        EAuditAction.UpdatedUserDetails,
                        EAuditDomain.UserAccount,
                        user.Id,
                        request.LoggedInUserIpAddress);

            return Response<string>.Ok(IdentityResponse.UserDetailsUpdated);
        }
    }
}