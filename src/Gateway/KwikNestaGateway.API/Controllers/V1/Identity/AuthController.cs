using Asp.Versioning;
using KwikNesta.Mediator.Cores.Abstractions;
using KwikNesta.Shared.Extensions;
using KwikNesta.Shared.Responses;
using KwikNesta.Shared.ServiceCommands.Identity;
using KwikNesta.Shared.ServiceDTOs.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KwikNestaGateway.API.Controllers.V1.Identity
{
    [Route("api/v{version:apiversion}/identity/auth")]
    [ApiVersion("1.0")]
    [ApiController]
    public class AuthController(IKNMediator mediator)
        : ControllerBase
    {
        private readonly IKNMediator _mediator = mediator;

        /// <summary>
        /// Signs in users
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("sign-in")]
        [ProducesResponseType(typeof(Response<LoginResponseDto>), 200)]
        public async Task<IActionResult> Login([FromBody] LoginCommand command)
        {
            command.UserIpAddress = HttpContext.GetUserIp();
            return Ok(await _mediator.SendAsync(command));
        }

        /// <summary>
        /// Signs up users
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("sign-up")]
        [ProducesResponseType(typeof(Response<RegistrationDto>), 200)]
        public async Task<IActionResult> Register([FromBody] RegistrationCommand command)
        {
            return Ok(await _mediator.SendAsync(command));
        }

        /// <summary>
        /// Verify account
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut("verify")]
        [ProducesResponseType(typeof(Response<string>), 200)]
        public async Task<IActionResult> Verify([FromBody] AccountVerificationCommand command)
        {
            var result = await _mediator.SendAsync(command);
            return Ok(result);
        }

        /// <summary>
        /// Requests new OTP
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("otp")]
        [ProducesResponseType(typeof(Response<string>), 200)]
        public async Task<IActionResult> RequestOtp([FromBody] ResendOtpCommand command)
        {
            var result = await _mediator.SendAsync(command);
            return Ok(result);
        }

        /// <summary>
        /// Refreshes token
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPatch("auth/token")]
        [ProducesResponseType(typeof(Response<LoginResponseDto>), 200)]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenCommand command)
        {
            var result = await _mediator.SendAsync(command);
            return Ok(result);
        }

        /// <summary>
        /// Change password
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPatch("password")]
        [ProducesResponseType(typeof(Response<string>), 200)]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordCommand command)
        {
            var userId = HttpContext.User.GetLoggedInUserId();
            if (string.IsNullOrWhiteSpace(userId))
            {
                return Forbid();
            }

            command.UserId = userId;
            command.UserIpAddress = HttpContext.GetUserIp();
            var result = await _mediator.SendAsync(command);
            return Ok(result);
        }

        /// <summary>
        /// Resets password
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPatch("reset-password")]
        [ProducesResponseType(typeof(Response<string>), 200)]
        public async Task<IActionResult> PasswordReset([FromBody] ResetPasswordCommand command)
        {
            var result = await _mediator.SendAsync(command);
            return Ok(result);
        }

        /// <summary>
        /// Change forgotten password
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPatch("forgot-password")]
        [ProducesResponseType(typeof(Response<string>), 200)]
        public async Task<IActionResult> ChangeForgot([FromBody] ForgotPasswordCommand command)
        {
            var result = await _mediator.SendAsync(command);
            return Ok(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPut("deactivate/{userId}")]
        [ProducesResponseType(typeof(Response<string>), 200)]
        public async Task<IActionResult> Deactivate([FromRoute] string userId)
        {
            var loggedInUserId = HttpContext.User.GetLoggedInUserId();
            var loggedInUserEmail = HttpContext.User.GetLoggedInUserEmail();
            var ipAddress = HttpContext.GetUserIp();
            if (string.IsNullOrWhiteSpace(loggedInUserEmail) || string.IsNullOrWhiteSpace(loggedInUserId))
            {
                return Forbid();
            }

            var result = await _mediator.SendAsync(new AccountDeactivationCommand
            {
                UserId = userId,
                LoggedInUserEmail = loggedInUserEmail,
                LoggedInUserId = loggedInUserId,
                UserIpAddress = ipAddress
            });
            return Ok(result);
        }

        /// <summary>
        /// Reactivation requests
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPatch("request-reactivation")]
        [ProducesResponseType(typeof(Response<string>), 200)]
        public async Task<IActionResult> RequestReactivation([FromBody] AccountReactivationRequestCommand command)
        {
            var result = await _mediator.SendAsync(command);
            return Ok(result);
        }

        /// <summary>
        /// Reactivates deactivated accounts
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPatch("reactivate")]
        [ProducesResponseType(typeof(Response<string>), 200)]
        public async Task<IActionResult> Reactivate([FromBody] AccountReactivationCommand command)
        {
            var result = await _mediator.SendAsync(command);
            return Ok(result);
        }

        /// <summary>
        /// Restores account status after suspension
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin, SuperAdmin")]
        [HttpPatch("admin/restore/{userId}")]
        [ProducesResponseType(typeof(Response<string>), 200)]
        public async Task<IActionResult> Restore([FromRoute] string userId)
        {
            return Ok(await _mediator.SendAsync(new AccountRestoreCommand
            {
                UserId = userId,
                LoggedInUserId = HttpContext.User.GetLoggedInUserId()!,
                LoggedInUserIpAddress = HttpContext.GetUserIp()
            }));
        }

        /// <summary>
        /// Suspends user account
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin, SuperAdmin")]
        [HttpPatch("admin/suspend")]
        [ProducesResponseType(typeof(Response<string>), 200)]
        public async Task<IActionResult> Suspend([FromBody] AccountSuspensionRequest request)
        {
            var result = await _mediator.SendAsync(new AccountSuspensionCommand
            {
                UserId = request.UserId,
                Reason = request.Reason,
                OtherReason = request.OtherReason,
                LoggedInUserId = HttpContext.User.GetLoggedInUserId()!,
                LoggedInUserIpAddress = HttpContext.GetUserIp()
            });
            return Ok(result);
        }
    }
}