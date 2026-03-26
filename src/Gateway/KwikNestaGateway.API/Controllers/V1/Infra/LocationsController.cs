using Asp.Versioning;
using KwikNesta.Mediator.Cores.Abstractions;
using KwikNesta.Shared.Extensions;
using KwikNesta.Shared.Responses;
using KwikNesta.Shared.ServiceCommands.Infra;
using KwikNesta.Shared.ServiceDTOs.Infra;
using KwikNesta.Shared.ServiceQueries.Infra;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KwikNestaGateway.API.Controllers.V1.Infra
{
    [Route("api/v{version:apiversion}/infra/locations")]
    [ApiVersion("1.0")]
    [ApiController]
    public class LocationsController(IKNMediator mediator) : ControllerBase
    {
        private readonly IKNMediator _mediator = mediator;

        /// <summary>
        /// Get paginated list of countries
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet("countries")]
        [ProducesResponseType(typeof(PagedResponse<CountryDto>), 200)]
        public async Task<IActionResult> GetPagedCountries([FromQuery] GetAllCountriesQuery query)
        {
            return Ok(await _mediator.SendAsync(query));
        }

        /// <summary>
        /// Gets country by id
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet("countries/{id}")]
        [ProducesResponseType(typeof(Response<CountryDto>), 200)]
        [ProducesResponseType(typeof(Response<string>), 404)]
        public async Task<IActionResult> GetCountryById([FromRoute] Guid id)
        {
            return Ok(await _mediator.SendAsync(new GetCountryByIdQuery
            {
                Id = id
            }));
        }

        /// <summary>
        /// Toggles country isActive status on/off
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "Admin, SuperAdmin")]
        [HttpPatch("countries/{id}/toggle")]
        [ProducesResponseType(typeof(Response<string>), 200)]
        [ProducesResponseType(typeof(Response<string>), 404)]
        public async Task<IActionResult> Toggle([FromRoute] Guid id)
        {
            return Ok(await _mediator.SendAsync(new ToggleCountryStatusCommand
            {
                Id = id,
                LoggedInUserId = HttpContext.User.GetLoggedInUserId()!,
                LoggedInUserEmail = HttpContext.User.GetLoggedInUserEmail()!,
                LoggedInUserIpAddress = HttpContext.GetUserIp()
            }));
        }

        /// <summary>
        /// Get paginated list of states by country id
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet("countries/{id}/states")]
        [ProducesResponseType(typeof(PagedResponse<StateDto>), 200)]
        public async Task<IActionResult> GetPagedStates([FromRoute] Guid id, 
                                                    [FromQuery] GetStatesByCountryClientQuery query)
        {
            return Ok(await _mediator.SendAsync(new GetStatesByCountryQuery
            {
                CountryId = id,
                Page = query.Page,
                PageSize = query.PageSize,
                Search = query.Search
            }));
        }

        /// <summary>
        /// Gets state by country and state ids
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet("countries/{id}/states/{stateId}")]
        [ProducesResponseType(typeof(Response<StateDto>), 200)]
        [ProducesResponseType(typeof(Response<string>), 404)]
        public async Task<IActionResult> GetCountryById([FromRoute] Guid id, [FromRoute] Guid stateId)
        {
            return Ok(await _mediator.SendAsync(new GetStateByIdQuery
            {
                CountryId = id,
                Id = stateId
            }));
        }

        /// <summary>
        /// Get paginated list of cities by country and state ids
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet("countries/{id}/states/{stateId}/cities")]
        [ProducesResponseType(typeof(PagedResponse<CityDto>), 200)]
        public async Task<IActionResult> GetPagedStates([FromRoute] Guid id,
                                                    [FromRoute] Guid stateId,
                                                    [FromQuery] GetCitiesByCountryAndStateClientQuery query)
        {
            return Ok(await _mediator.SendAsync(new GetCitiesByCountryAndStateQuery
            {
                CountryId = id,
                StateId = stateId,
                Page = query.Page,
                PageSize = query.PageSize,
                Search = query.Search
            }));
        }
    }
}