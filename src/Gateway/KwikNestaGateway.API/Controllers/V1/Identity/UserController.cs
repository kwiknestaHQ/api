using Asp.Versioning;
using KwikNesta.Mediator.Cores.Abstractions;
using KwikNesta.Shared.Extensions;
using KwikNesta.Shared.Responses;
using KwikNesta.Shared.ServiceCommands.Identity;
using KwikNesta.Shared.ServiceDTOs.Identity;
using KwikNesta.Shared.ServiceQueries.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KwikNestaGateway.API.Controllers.V1.Identity
{
    [Route("api/v{version:apiversion}/identity/users")]
    [ApiVersion("1.0")]
    [ApiController]
    public class UserController(IKNMediator mediator)
                : ControllerBase
    {
        private readonly IKNMediator _mediator = mediator;

        /// <summary>
        /// Get logged in user details
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet("current")]
        [ProducesResponseType(typeof(Response<CurrentUserDto>), 200)]
        public async Task<IActionResult> Current()
        {
            var userId = HttpContext.User.GetLoggedInUserId();
            if (string.IsNullOrWhiteSpace(userId))
            {
                return Forbid();
            }

            return Ok(await _mediator.SendAsync(new LoggedInUserQuery
            {
                UserId = userId
            }));
        }

        /// <summary>
        /// Updates user basic details
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPut("update")]
        [ProducesResponseType(typeof(Response<string>), 200)]
        public async Task<IActionResult> UpdateDetails([FromBody] BasicUserDetailsUpdateRequest request)
        {
            return Ok(await _mediator.SendAsync(new BasicUserDetailsUpdateCommand
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                OtherName = request.OtherName,
                Gender = request.Gender,
                LoggedInUserId = HttpContext.User.GetLoggedInUserId()!,
                LoggedInUserIpAddress = HttpContext.GetUserIp()
            }));
        }

        /// <summary>
        /// Get user record by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [Authorize]
        [ProducesResponseType(typeof(Response<CurrentUserDto>), 200)]
        public async Task<IActionResult> GetById([FromRoute] string id)
        {
            var response = await _mediator.SendAsync(new GetUserByIdQuery
            {
                Id = id
            });
            return Ok(response);
        }

        /// <summary>
        /// Get paginated list of users
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "Admin, SuperAdmin")]
        [ProducesResponseType(typeof(PagedResponse<CurrentUserDto>), 200)]
        public async Task<IActionResult> GetPaged([FromQuery] GetPagedUsersQuery query)
        {
            var response = await _mediator.SendAsync(query);
            return Ok(response);
        }
    }
}