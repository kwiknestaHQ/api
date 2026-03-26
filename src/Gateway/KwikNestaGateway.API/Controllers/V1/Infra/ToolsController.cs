using Asp.Versioning;
using KwikNesta.Mediator.Hangfire.Abstractions;
using KwikNesta.Shared.Contracts;
using KwikNesta.Shared.Extensions;
using KwikNesta.Shared.Responses;
using KwikNesta.Shared.ServiceNotifications.Infra;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KwikNestaGateway.API.Controllers.V1.Infra
{
    [Route("api/v{version:apiversion}/infra/tools")]
    [ApiVersion("1.0")]
    [ApiController]
    public class ToolsController(IKNBackgroundMediator bgMediator,
                                IUploadService uploadService) : ControllerBase
    {
        private readonly IKNBackgroundMediator _bgMediator = bgMediator;
        private readonly IUploadService _uploadService = uploadService;

        /// <summary>
        /// Runs Location Dataload
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "Admin, SuperAdmin")]
        [HttpPost("location-dataload")]
        [ProducesResponseType(typeof(Response<string>), 200)]
        public IActionResult RunLocationDataload()
        {
            _bgMediator.Publish(new MigrateCsDataNotification
            {
                LoggedInUserId = HttpContext.User.GetLoggedInUserId()!,
                LoggedInUserEmail = HttpContext.User.GetLoggedInUserEmail()!,
                LoggedInUserIpAddress = HttpContext.GetUserIp()
            });

            return Ok(Response<string>.Ok("Location Dataload started. You'll be notified once completed."));
        }

        /// <summary>
        /// Endpoint to upload an asset
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin, SuperAdmin")]
        [HttpPost("asset")]
        [ProducesResponseType(typeof(Response<string>), 200)]
        public async Task<IActionResult> UploadAsset(IFormFile file)
        {
            var extension = Path.GetExtension(file.FileName).ToLower();
            var fileType = FileExtensions.GetFileType(extension);
            if (!file.IsValidFile(fileType))
            {
                return BadRequest(Response<string>.Fail("Invalid file or size exceeds the acceptable limits", 400));
            }

            var bytes = file.GetBytes();
            var detectedType = bytes.DetectContentType();
            if (!FileValidation.IsValid(detectedType, extension))
            {
                return BadRequest(Response<string>.Fail("Invalid file type", 400));
            }

            return Ok(Response<string>.Ok(
                await _uploadService.UploadFileAsync(bytes, extension, detectedType, fileType)));
        }

        /// <summary>
        /// Endpoint to delete an uploaded asset
        /// </summary>
        /// <param name="assetUrl"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin, SuperAdmin")]
        [HttpDelete("asset")]
        [ProducesResponseType(typeof(Response<string>), 200)]
        public async Task<IActionResult> DeleteAsset([FromForm] string assetUrl)
        {
            await _uploadService.DeleteByUrlAsync(assetUrl);
            return Ok(Response<string>.Ok("Asset successfully deleted"));
        }
    }
}