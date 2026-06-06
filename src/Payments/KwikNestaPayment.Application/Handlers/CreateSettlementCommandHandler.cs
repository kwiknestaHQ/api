using KwikNesta.Mediator.Cores.Abstractions;
using KwikNesta.Shared.Extensions;
using KwikNesta.Shared.Models.Enumerations.Payments;
using KwikNesta.Shared.Responses;
using KwikNesta.Shared.ServiceCommands.Payment;
using KwikNesta.Shared.ServiceQueries.Identity;
using KwikNesta.Shared.ServiceQueries.Property;
using KwikNestaPayment.Application.Validations;
using KwikNestaPayment.Domain.Entities;
using KwikNestaPayment.Infrastructure;
using Microsoft.AspNetCore.Http;

namespace KwikNestaPayment.Application.Handlers
{
    public class CreateSettlementCommandHandler(IPaymentRepositoryManager repository, 
                    IKNMediator mediator) 
        : IKNRequestHandler<CreateSettlementCommand, Response<string>>
    {
        private readonly IPaymentRepositoryManager _repository = repository;
        private readonly IKNMediator _mediator = mediator;

        public async Task<Response<string>> HandleAsync(CreateSettlementCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateSettlementCommandValidator().Validate(request);
            if (!validator.IsValid)
            {
                return Response<string>.Fail(validator.Errors.FirstOrDefault()?.ErrorMessage ?? 
                    "Invalid request", 
                    StatusCodes.Status400BadRequest);
            }

            var user = await _mediator.SendAsync(new GetUserByIdQuery
            {
                Id = request.UserId
            }, cancellationToken);

            if (!user.Success || user.Data == null)
            {
                return Response<string>.Fail(user.Message, user.StatusCode);
            }

            var isValidReferenceEntity = await IsValidReference(request.ReferenceId, request.Purpose);
            if (!isValidReferenceEntity)
            {
                return Response<string>.Fail(
                    string.Format(PaymentResponses.RecordNotFound, 
                        request.Purpose.GetDescription()),
                    StatusCodes.Status404NotFound);
            }

            await _repository.Settlement
                .AddAsync(new KNSettlement
                {
                    Amount = request.Amount,
                    BeneficiaryId = request.UserId,
                    Purpose = request.Purpose,
                    ReferenceId = request.ReferenceId,
                    Type = request.SettlementType
                });

            await _repository.SaveAsync();
            return Response<string>.Ok("Settlement successfully created and logged for execution.");
        }

        private async Task<bool> IsValidReference(Guid referenceId, EPaymentPurpose purpose)
        {
            var isValid = false;
            switch (purpose)
            {
                case EPaymentPurpose.Viewing:
                    var request = await _mediator.SendAsync(new GetVerificationRequestByIdQuery
                    {
                        RequestId = referenceId,
                    });
                    
                    isValid = request.Success;
                break;

                default: break;
            };

            return isValid;
        }
    }
}