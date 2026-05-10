using Asp.Versioning;
using Hangfire;
using KwikNesta.Shared.Extensions;
using KwikNesta.Shared.Models.Settings;
using KwikNestaInfra.Infrastructure.Contracts;
using KwikNestaPayment.Infrastructure.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Text;
using System.Text.Json;

namespace KwikNestaGateway.API.Controllers.V1
{
    [Route("api/v{version:apiversion}/webhooks")]
    [ApiVersion("1.0")]
    [ApiController]
    public class WebhooksController(IOptions<KNApplicationSettings> options) : ControllerBase
    {
        private readonly string paystackSecret = options.Value.Paystack.PrivateKey;
        private readonly string agoraSecret = options.Value.Agora.WebhookSecret;

        [HttpPost("paystack")]
        public async Task<IActionResult> HandlePaystackWebhook()
        {
            var body = await ReadBody(Request);

            if (!PaymentExtensions.IsValidPaystckSignature(body, 
                Request.Headers["x-paystack-signature"]!, 
                paystackSecret))
            {
                return Unauthorized();
            }

            BackgroundJob.Enqueue<IPaymentWebhookService>(job 
                => job.ProcessPaystackWebhook(body, null!));

            return Ok();
        }

        [HttpPost("agora")]
        public async Task<IActionResult> HandleAgoraWebhook()
        {
            var body = await ReadBody(Request);
            var signature = Request.Headers["Agora-Signature-V2"].ToString();
            if (!PaymentExtensions.VerifySignature(body, agoraSecret, signature))
            {
                return Unauthorized();
            }

            BackgroundJob.Enqueue<IVirtualMediaCallService>(job
               => job.ProcessAgoraWebhook(body, null!));

            return Ok();
        }

        #region Private Methods
        private async Task<string> ReadBody(HttpRequest request)
        {
            request.EnableBuffering();

            using var reader = new StreamReader(
                request.Body,
                Encoding.UTF8,
                detectEncodingFromByteOrderMarks: false,
                leaveOpen: true);

            var body = await reader.ReadToEndAsync();

            request.Body.Position = 0;

            return body;
        }

        #endregion
    }
}