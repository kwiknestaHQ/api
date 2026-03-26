using Asp.Versioning;
using KwikNesta.Mediator.Cores.Abstractions;
using KwikNesta.Shared.Responses;
using KwikNesta.Shared.ServiceDTOs.Infra;
using KwikNesta.Shared.ServiceQueries.Infra;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KwikNestaGateway.API.Controllers.V1.Infra
{
    [Route("api/v{version:apiversion}/infra/audit")]
    [ApiVersion("1.0")]
    [ApiController]
    public class AuditController(IKNMediator mediator) : ControllerBase
    {
        private readonly IKNMediator _mediator = mediator;

        /// <summary>
        /// Get audit logs
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet("{entityId}")]
        [ProducesResponseType(typeof(PagedResponse<AuditLogResponseDto>), 200)]
        public async Task<IActionResult> GetPaged([FromRoute] string entityId,
                                                [FromQuery] AuditLogClientQuery query)
        {
            return Ok(await _mediator.SendAsync(new AuditLogQuery
            {
                Page = query.Page,
                PageSize = query.PageSize,
                Action = query.Action,
                DomainId = entityId
            }));
        }

        /// <summary>
        /// Get system audit logs
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "Admin, SuperAdmin")]
        [HttpGet("system")]
        [ProducesResponseType(typeof(PagedResponse<AuditLogResponseDto>), 200)]
        public async Task<IActionResult> GetAdminPaged([FromQuery] AdminAuditLogClientQuery query)
        {
            return Ok(await _mediator.SendAsync(query));
        }
    }
}
