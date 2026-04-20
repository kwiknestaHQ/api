using Asp.Versioning;
using KwikNesta.Mediator.Cores.Abstractions;
using KwikNesta.Shared.Extensions;
using KwikNesta.Shared.Responses;
using KwikNesta.Shared.ServiceCommands.Property;
using KwikNesta.Shared.ServiceDTOs.Property;
using KwikNesta.Shared.ServiceQueries.Property;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace KwikNestaGateway.API.Controllers.V1.Property
{
    [Route("api/v{version:apiversion}/properties")]
    [ApiVersion("1.0")]
    [ApiController]
    public class PropertiesController(IKNMediator mediator) : ControllerBase
    {
        private readonly IKNMediator _mediator = mediator;

        /// <summary>
        /// Endpoint to search for properties
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(PagedResponse<PropertyCardDto>), 200)]
        public async Task<IActionResult> Search([FromQuery] PropertySearchQuery query)
        {
            return Ok(await _mediator.SendAsync(query));
        }

        /// <summary>
        /// Gets property details by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Response<PropertyDetailsDto>), 200)]
        public async Task<IActionResult> GetDetails([FromRoute] Guid id)
        {
            return Ok(await _mediator.SendAsync(new GetPropertyDetailsQuery
            {
                Id = id,
                Context = HttpContext.GetContext()
            }));
        }

        /// <summary>
        /// Create property draft
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "LandLord")]
        [ProducesResponseType(typeof(Response<CreatePropertyResponseDto>), 200)]
        public async Task<IActionResult> CreateDraft([FromBody] CreatePropertyRequest request)
        {
            return Ok(await _mediator.SendAsync(new CreatePropertyCommand
            {
                Title = request.Title,
                Description = request.Description,
                Price = request.Price,
                Type = request.Type,
                ListingType = request.ListingType,
                PriceFrequency = request.PriceFrequency,
                Bathrooms = request.Bathrooms,
                Bedrooms = request.Bedrooms,
                AreaSize = request.AreaSize,
                ParkingSpaces = request.ParkingSpaces,
                Location = request.Location,
                UserContext = HttpContext.GetContext()
            }));
        }

        /// <summary>
        /// Add/Update property features
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("{id}/features")]
        [Authorize(Roles = "LandLord")]
        [ProducesResponseType(typeof(Response<CreatePropertyResponseDto>), 200)]
        public async Task<IActionResult> AddOrUpdateFeatures([FromRoute] Guid id, 
                                                            [FromBody] UpdatePropertyFeaturesRequest request)
        {
            return Ok(await _mediator.SendAsync(new UpdatePropertyFeaturesCommand
            {
                PropertyId = id,
                FeatureIds = request.FeatureIds,
                CustomFeatures = request.CustomFeatures,
                UserContext = HttpContext.GetContext()
            }));
        }

        /// <summary>
        /// Add property media
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("{id}/media")]
        [Authorize(Roles = "LandLord")]
        [ProducesResponseType(typeof(Response<CreatePropertyResponseDto>), 200)]
        public async Task<IActionResult> UploadPropertyMedia([FromRoute] Guid id,
                                                            [FromForm] UploadPropertyMediaRequest request)
        {
            return Ok(await _mediator.SendAsync(new UploadPropertyMediaCommand
            {
                PropertyId = id,
                Files = request.Files,
                UserContext = HttpContext.GetContext()
            }));
        }

        /// <summary>
        /// Submits property verification request
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request">
        /// <br/><br/>
        /// <b>Accepted values for DocumentType: </b>
        /// <list type="bullet">
        /// <item><description><c>1</c> = Title Deed, </description></item>
        /// <item><description><c>2</c> = Utility Bill,</description></item>
        /// <item><description><c>3</c> = Government Id,</description></item>
        /// <item><description><c>4</c> = Purchase Receipt,</description></item>
        /// <item><description><c>99</c> = Other</description></item>
        /// <br/><br/>
        /// </list>
        /// </param>
        /// <returns></returns>
        [HttpPost("{id}/verification")]
        [Authorize(Roles = "LandLord")]
        [ProducesResponseType(typeof(Response<string>), 200)]
        public async Task<IActionResult> Submit([FromRoute] Guid id,
                                              [FromForm] SubmitVerificationRequest request)
        {
            return Ok(await _mediator.SendAsync(new SubmitVerificationCommand
            {
                PropertyId = id,
                Documents = request.Documents,
                UserContext = HttpContext.GetContext()
            }));
        }

        /// <summary>
        /// Updates property basic details
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("{id}/basic-info")]
        [Authorize(Roles = "LandLord")]
        [ProducesResponseType(typeof(Response<string>), 200)]
        public async Task<IActionResult> UpdateInfo([FromRoute] Guid id,
                                              [FromBody] UpdatePropertyBasicInfoRequest request)
        {
            return Ok(await _mediator.SendAsync(new UpdatePropertyBasicInfoCommand
            {
                PropertyId = id,
                Title = request.Title,
                Description = request.Description,
                Type = request.Type,
                ListingType = request.ListingType,
                PriceFrequency = request.PriceFrequency,
                Price = request.Price,
                Bedrooms = request.Bedrooms,
                Bathrooms = request.Bathrooms,
                AreaSize = request.AreaSize,
                ParkingSpaces = request.ParkingSpaces,
                UserContext = HttpContext.GetContext()
            }));
        }

        /// <summary>
        /// Updates property basic details
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("{id}/location-info")]
        [Authorize(Roles = "LandLord")]
        [ProducesResponseType(typeof(Response<string>), 200)]
        public async Task<IActionResult> UpdateLocation([FromRoute] Guid id,
                                              [FromBody] UpdatePropertyLocationRequest request)
        {
            return Ok(await _mediator.SendAsync(new UpdatePropertyLocationCommand
            {
                PropertyId = id,
                Address = request.Address,
                City = request.City,
                CountryId = request.CountryId,
                Latitude = request.Latitude,
                Longitude = request.Longitude,
                StateId = request.StateId,
                UserContext = HttpContext.GetContext()
            }));
        }

        /// <summary>
        /// Creates property inspection request
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("{id}/view-request")]
        [Authorize(Roles = "Tenant")]
        [ProducesResponseType(typeof(Response<string>), 200)]
        public async Task<IActionResult> CreateViewRequest([FromRoute] Guid id, 
                                                        CreateViewingRequestDto request)
        {
            return Ok(await _mediator.SendAsync(new CreateViewingRequestCommand
            {
                PropertyId = id,
                Type = request.Type,
                ScheduledDate = request.ScheduledDate,
                Note = request.Note,
                Context = HttpContext.GetContext()
            }));
        }
    }
}