using KwikNesta.Mediator.Cores.Abstractions;
using KwikNesta.Shared.Models.Enumerations.Property;
using KwikNesta.Shared.Responses;
using KwikNesta.Shared.ServiceCommands.Property;
using KwikNestaProperty.Infrastructure;
using Microsoft.AspNetCore.Http;

namespace KwikNestaProperty.Application.Handlers
{
    public class UpdateViewRequestAndSessionOnSettlementCommandHandler(IPropertyRepositoryManager repository) :
        IKNRequestHandler<UpdateViewRequestAndSessionOnSettlementCommand, Response<string>>
    {
        private readonly IPropertyRepositoryManager _repository = repository;

        public async Task<Response<string>> HandleAsync(UpdateViewRequestAndSessionOnSettlementCommand request, CancellationToken cancellationToken)
        {
            if(request.ViewRequestId == Guid.Empty)
            {
                Response<string>.Fail(PropertyResponse.InvalidRequest, 
                    StatusCodes.Status400BadRequest);
            }

            var viewRequest = await _repository.ViewingRequest
                .FirstOrDefault(vr => vr.Id == request.ViewRequestId, true);

            if(viewRequest == null)
            {
                return Response<string>.Fail(
                    string.Format(PropertyResponse.RecordNotFound, "View Request"),
                    StatusCodes.Status404NotFound);
            }

            if (!viewRequest.SessionId.HasValue)
            {
                return Response<string>.Fail(PropertyResponse.InvalidSessionId, 
                    StatusCodes.Status400BadRequest);
            }

            var session = await _repository.ViewingSession
                .FirstOrDefault(vr => vr.Id == viewRequest.SessionId.Value, true);

            if (session == null)
            {
                return Response<string>.Fail(
                    string.Format(PropertyResponse.RecordNotFound, "View Session"),
                    StatusCodes.Status404NotFound);
            }

            session.UpdateSettlementStatus(ESSessionettlementStatus.Completed);
            await _repository.SaveAsync();

            return Response<string>.Ok("View Request and Session successfully updated");
        }
    }
}