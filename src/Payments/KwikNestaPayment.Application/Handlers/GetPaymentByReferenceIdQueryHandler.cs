using KwikNesta.Mediator.Cores.Abstractions;
using KwikNesta.Shared.Responses;
using KwikNesta.Shared.ServiceDTOs.Payment;
using KwikNesta.Shared.ServiceQueries.Payment;
using KwikNestaPayment.Infrastructure;
using Microsoft.AspNetCore.Http;

namespace KwikNestaPayment.Application.Handlers
{
    public class GetPaymentByReferenceIdQueryHandler(IPaymentRepositoryManager repository) : IKNRequestHandler<GetPaymentByReferenceIdQuery, Response<PaymentDto>>
    {
        private readonly IPaymentRepositoryManager _repository = repository;

        public async Task<Response<PaymentDto>> HandleAsync(GetPaymentByReferenceIdQuery request, CancellationToken cancellationToken)
        {
            if (request.ReferenceId == Guid.Empty)
            {
                return Response<PaymentDto>.Fail(PaymentResponses.InvalidRequest,
                    StatusCodes.Status400BadRequest);
            }

            var payment = await _repository.Payment
                .FirstOrDefault(p => p.ReferenceId == request.ReferenceId);
            if (payment == null)
            {
                return Response<PaymentDto>.Fail(string.Format(PaymentResponses.RecordNotFound, "Payment"),
                    StatusCodes.Status404NotFound);
            }

            return Response<PaymentDto>.Ok(payment.Map());
        }
    }
}