using KwikNesta.Mediator.Cores.Abstractions;
using KwikNesta.Shared.Responses;
using KwikNesta.Shared.ServiceCommands.Property;
using KwikNestaProperty.Application.Validations;
using KwikNestaProperty.Infrastructure;
using Microsoft.AspNetCore.Http;

namespace KwikNestaProperty.Application.Handlers
{
    public class UpdateViewingRequestPaymentStatusCommandHandler(IPropertyRepositoryManager repository) :
        IKNRequestHandler<UpdateViewingRequestPaymentStatusCommand, Response<string>>
    {
        private readonly IPropertyRepositoryManager _repository = repository;

        public async Task<Response<string>> HandleAsync(UpdateViewingRequestPaymentStatusCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateViewingRequestPaymentStatusCommandValidator().Validate(request);
            if (!validator.IsValid)
            {
                return Response<string>.Fail(validator.Errors.FirstOrDefault()?.ErrorMessage ??
                    PropertyResponse.InvalidRequest, 400);
            }

            var viewingRequest = await _repository.ViewingRequest
                .FirstOrDefault(vr => vr.Id == request.Id, true);

            if (viewingRequest == null)
            {
                return Response<string>.Fail(string.Format(PropertyResponse.RecordNotFound, 
                    "View Request"),
                    StatusCodes.Status404NotFound);
            }

            if(viewingRequest.PaymentStatus == request.NewStatus)
            {
                return Response<string>.Fail(PropertyResponse.RequestPaymentStatusAlreadyInStatus,
                    StatusCodes.Status409Conflict);
            }

            viewingRequest.PaymentStatus = request.NewStatus;
            await _repository.SaveAsync();

            return Response<string>.Ok(PropertyResponse.PaymentStatusUpdated);
        }
    }
}