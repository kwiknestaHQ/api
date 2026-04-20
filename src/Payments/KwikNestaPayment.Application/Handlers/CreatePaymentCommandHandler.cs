using KwikNesta.Mediator.Cores.Abstractions;
using KwikNesta.Shared.Implementations;
using KwikNesta.Shared.Models.Enumerations.Infra;
using KwikNesta.Shared.Responses;
using KwikNesta.Shared.ServiceCommands.Payment;
using KwikNesta.Shared.ServiceDTOs.Payment;
using KwikNestaPayment.Application.Validations;
using KwikNestaPayment.Infrastructure;
using Microsoft.AspNetCore.Http;

namespace KwikNestaPayment.Application.Handlers
{
    public class CreatePaymentCommandHandler(IPaymentRepositoryManager repository) : IKNRequestHandler<CreatePaymentCommand, Response<PaymentDto>>
    {
        private readonly IPaymentRepositoryManager _repository = repository;

        public async Task<Response<PaymentDto>> HandleAsync(CreatePaymentCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreatePaymentCommandValidator().Validate(request);
            if (!validator.IsValid)
            {
                return Response<PaymentDto>.Fail(PaymentResponses.InvalidRequest, StatusCodes.Status400BadRequest);
            }

            var payment = request.Map();
            await _repository.Payment.AddAsync(payment);
            await _repository.SaveAsync();

            AppAudit.Write(request.Context.Id,
                request.Context.Email,
                EAuditAction.AddedPaymentIntent,
                EAuditDomain.Payment,
                payment.Id.ToString(),
                request.Context.IpAddress);

            return Response<PaymentDto>.Ok(payment.Map());
        }
    }
}