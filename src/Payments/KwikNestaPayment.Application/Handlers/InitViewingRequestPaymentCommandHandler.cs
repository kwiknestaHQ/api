using KwikNesta.Mediator.Cores.Abstractions;
using KwikNesta.Shared.Extensions;
using KwikNesta.Shared.Models.Enumerations.Payments;
using KwikNesta.Shared.Responses;
using KwikNesta.Shared.ServiceCommands.Payment;
using KwikNesta.Shared.ServiceDTOs.Property;
using KwikNesta.Shared.ServiceQueries.Property;
using KwikNestaPayment.Infrastructure;

namespace KwikNestaPayment.Application.Handlers
{
    public class InitViewingRequestPaymentCommandHandler(IPaymentRepositoryManager repository, 
                                                    IKNMediator mediator) 
        : IKNRequestHandler<InitViewingRequestPaymentCommand, Response<ViewRequestPaymentInitResult>>
    {
        private readonly IPaymentRepositoryManager _repository = repository;
        private readonly IKNMediator _mediator = mediator;

        public async Task<Response<ViewRequestPaymentInitResult>> HandleAsync(InitViewingRequestPaymentCommand request, CancellationToken cancellationToken)
        {
            var viewRequest = await _mediator.SendAsync(new GetViewRequestByIdQuery
            {
                Id = request.Id,
            }, cancellationToken);

            if (!viewRequest.Success)
            {
                return Response<ViewRequestPaymentInitResult>.Fail(viewRequest.Message, viewRequest.StatusCode);
            }

            var viewRequestMode = viewRequest.Data;
            var paymentModelResult = await _mediator.SendAsync(new CreatePaymentCommand
            {
                Context = request.Context,
                Amount = viewRequestMode.Fee,
                NetAmount = viewRequestMode.Fee,
                PlatformFee = 0,
                Purpose = EPaymentPurpose.Viewing,
                ReferenceId = request.Id,
                Reference = PaymentExtensions.GenerateReference(EPaymentCode.ViewRequest)
            }, cancellationToken);

            if (!paymentModelResult.Success)
            {
                return Response<ViewRequestPaymentInitResult>.Fail(paymentModelResult.Message, paymentModelResult.StatusCode);
            }

            var payStackResponse = await _mediator.SendAsync(new PaystackChargeCommand
            {
                Amount = paymentModelResult.Data.NetAmount.ToMinorUnits(),
                Context = request.Context,
                Email = request.Context.Email,
                Reference = paymentModelResult.Data.Reference
            }, cancellationToken);

            if (!payStackResponse.Success)
            {
                return Response<ViewRequestPaymentInitResult>.Fail(payStackResponse.Message, payStackResponse.StatusCode);
            }

            var paystackResult = payStackResponse.Data.Data;
            return Response<ViewRequestPaymentInitResult>.Ok(new ViewRequestPaymentInitResult
            {
                AuthorizationUrl = paystackResult.AuthorizationUrl,
                PaymentCode = paystackResult.AccessCode,
                Reference = paystackResult.Reference
            });
        }
    }
}