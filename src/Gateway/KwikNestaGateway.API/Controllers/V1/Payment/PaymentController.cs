using Asp.Versioning;
using KwikNesta.Mediator.Cores.Abstractions;
using KwikNesta.Shared.Extensions;
using KwikNesta.Shared.Responses;
using KwikNesta.Shared.ServiceCommands.Payment;
using KwikNesta.Shared.ServiceDTOs.Payment;
using KwikNesta.Shared.ServiceQueries.Payment;
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
        [ProducesResponseType(typeof(Response<ViewRequestPaymentInitResult>), StatusCodes.Status200OK)]
        public async Task<IActionResult> InitializeViewingPayment(Guid requestId)
        {
            return Ok(await _mediator.SendAsync(new InitViewingRequestPaymentCommand
            {
                Id = requestId,
                Context = HttpContext.GetContext()
            }));
        }

        [Authorize]
        [HttpGet("paystack/banks")]
        [ProducesResponseType(typeof(Response<List<PaystackBanksResponseData>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetBanks()
        {
            return Ok(await _mediator.SendAsync(new GetPaystackBanksQuery()));
        }

        [Authorize]
        [HttpGet("paystack/accounts/resolve")]
        [ProducesResponseType(typeof(Response<PaystackAccountResolutionResult>), StatusCodes.Status200OK)]
        public async Task<IActionResult> ResolveAccount([FromQuery] ResolvePaystackBankAccountQuery query)
        {
            return Ok(await _mediator.SendAsync(query));
        }

        [HttpPost("users/bank-accounts")]
        [Authorize(Roles = "Tenant, LandLord")]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status200OK)]
        public async Task<IActionResult> AddAccount([FromBody] AddUserBankAccountRequest request)
        {
            return Ok(await _mediator.SendAsync(new AddUserBankAccountCommand
            {
                Context = HttpContext.GetContext(),
                AccountNumber = request.AccountNumber,
                BankCode = request.BankCode,
                BankName = request.BankName,
                Type = request.Type,
                Currency = request.Currency
            }));
        }
    }
}