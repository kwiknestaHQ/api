using Asp.Versioning;
using KwikNesta.Mediator.Cores.Abstractions;
using KwikNesta.Shared.Extensions;
using KwikNesta.Shared.ServiceCommands.Payment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KwikNestaGateway.API.Controllers.V1.Payment
{
    [Route("api/v{version:apiversion}/payment")]
    [ApiVersion("1.0")]
    [ApiController]
    public class PaymentController(IKNMediator mediator) : ControllerBase
    {
        private readonly IKNMediator _mediator = mediator;

        [Authorize]
        [HttpPost("paystack/viewing/{requestId}/init")]
        public async Task<IActionResult> InitializeViewingPayment(Guid requestId)
        {
            return Ok(await _mediator.SendAsync(new InitViewingRequestPaymentCommand
            {
                Id = requestId,
                Context = HttpContext.GetContext()
            }));
        }
    }
}