using Asp.Versioning;
using KwikNesta.Mediator.Cores.Abstractions;
using KwikNesta.Shared.Extensions;
using KwikNesta.Shared.Responses;
using KwikNesta.Shared.ServiceCommands.Property;
using KwikNesta.Shared.ServiceDTOs.Property;
using KwikNesta.Shared.ServiceQueries.Property;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KwikNestaGateway.API.Controllers.V1.Property
{
    [Route("api/v{version:apiversion}/properties/admin")]
    [ApiVersion("1.0")]
    [ApiController]
    [Authorize(Roles = "Admin, SuperAdmin")]
    public class PropertiesAdminController(IKNMediator mediator) : ControllerBase
    {
        private readonly IKNMediator _mediator = mediator;

        /// <summary>
        /// Review property verification request
        /// </summary>
        /// <param name="id"></param>
        /// <param name="requestId"></param>
        /// <param name="request">
        /// <br/><br/>
        /// <b>Accepted values for Action: </b>
        /// <list type="bullet">
        /// <item><description><c>0</c> = Approve, </description></item>
        /// <item><description><c>1</c> = Reject</description></item>
        /// <br/><br/>
        /// </list>
        /// </param>
        /// <returns></returns>
        [HttpPut("{id}/review/{requestId}")]
        [ProducesResponseType(typeof(Response<string>), 200)]
        public async Task<IActionResult> Review([FromRoute] Guid id,
                                              [FromRoute] Guid requestId,
                                              [FromBody] ReviewVerificationRequest request)
        {
            return Ok(await _mediator.SendAsync(new ReviewVerificationCommand
            {
                PropertyId = id,
                RequestId = requestId,
                Action = request.Action,
                Reason = request.Reason,
                UserContext = HttpContext.GetContext()
            }));
        }

        /// <summary>
        /// Gets paginated list of property verification requests
        /// </summary>
        /// <param name="query">
        /// <br/><br/>
        /// <b>Accepted values for Status: </b>
        /// <list type="bullet">
        /// <item><description><c>0</c> = Pending, </description></item>
        /// <item><description><c>1</c> = Approve, </description></item>
        /// <item><description><c>2</c> = Declined</description></item>
        /// <br/><br/>
        /// </list>
        /// </param>
        /// <returns></returns>
        [HttpGet("verification-requests")]
        [ProducesResponseType(typeof(PagedResponse<VerificationRequestLeanDto>), 200)]
        public async Task<IActionResult> VerificationRequests([FromQuery] GetVerificationRequestsQuery query)
        {
            return Ok(await _mediator.SendAsync(query));
        }

        /// <summary>
        /// Gets property verification request details y request id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("verification-requests/{id}")]
        [ProducesResponseType(typeof(Response<VerificationRequestDto>), 200)]
        public async Task<IActionResult> VerificationRequest([FromRoute] Guid id)
        {
            return Ok(await _mediator.SendAsync(new GetVerificationRequestByIdQuery
            {
                RequestId = id
            }));
        }
    }
}