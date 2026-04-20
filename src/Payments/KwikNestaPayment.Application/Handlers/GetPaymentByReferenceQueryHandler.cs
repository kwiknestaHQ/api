using KwikNesta.Mediator.Cores.Abstractions;
using KwikNesta.Shared.Responses;
using KwikNesta.Shared.ServiceDTOs.Payment;
using KwikNesta.Shared.ServiceQueries.Payment;
using KwikNestaPayment.Infrastructure;
using Microsoft.AspNetCore.Http;

namespace KwikNestaPayment.Application.Handlers
{
    public class GetPaymentByReferenceQueryHandler(IPaymentRepositoryManager repository) 
        : IKNRequestHandler<GetPaymentByReferenceQuery, Response<PaymentDto>>
    {
        private readonly IPaymentRepositoryManager _repository = repository;

        public async Task<Response<PaymentDto>> HandleAsync(GetPaymentByReferenceQuery request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.Reference))
            {
                return Response<PaymentDto>.Fail(PaymentResponses.InvalidRequest,
                    StatusCodes.Status400BadRequest);
            }

            var payment = await _repository.Payment
                .FirstOrDefault(p => p.Reference == request.Reference);
            if (payment == null)
            {
                return Response<PaymentDto>.Fail(string.Format(PaymentResponses.RecordNotFound, "Payment"), 
                    StatusCodes.Status404NotFound);
            }

            return Response<PaymentDto>.Ok(new PaymentDto
            {
                Id = payment.Id,
                Reference = payment.Reference,
                Amount = payment.Amount,
                NetAmount = payment.NetAmount,
                PaidAt = payment.PaidAt,
                CreatedOn = payment.CreatedOn,
                Currency = payment.Currency,
                PlatformFee = payment.PlatformFee,
                Purpose = payment.Purpose,
                ReferenceId = payment.ReferenceId,
                Status = payment.Status
            });
        }
    }
}
