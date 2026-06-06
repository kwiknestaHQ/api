using KwikNesta.Mediator.Cores.Abstractions;
using KwikNesta.Shared.Extensions;
using KwikNesta.Shared.Responses;
using KwikNesta.Shared.ServiceCommands.Property;
using KwikNestaProperty.Infrastructure;
using Microsoft.AspNetCore.Http;

namespace KwikNestaProperty.Application.Handlers
{
    public class ViewSessionUserLeftCommandHandler(IPropertyRepositoryManager repository) 
        : IKNRequestHandler<ViewSessionUserLeftCommand, Response<string>>
    {
        private readonly IPropertyRepositoryManager _repository = repository;

        public async Task<Response<string>> HandleAsync(ViewSessionUserLeftCommand request, CancellationToken cancellationToken)
        {
            var session = await _repository.ViewingSession
               .FirstOrDefault(vs => vs.ChannelName == request.ChannelName);
            if (session == null)
            {
                return Response<string>.Fail(string.Format(PropertyResponse.RecordNotFound, "Viewing Session"),
                    StatusCodes.Status404NotFound);
            }

            var participant = await _repository.SessionParticipant
                .FirstOrDefault(sp => sp.ViewingSessionId == session.Id &&
                            sp.UId == request.UId &&
                            sp.Role == request.Role, true);
            if (participant == null)
            {
                return Response<string>.Fail(string.Format(PropertyResponse.RecordNotFound, "Session Participant"),
                   StatusCodes.Status404NotFound);
            }

            participant.MarkAsLeft(request.Timestamp, request.Duration);
            await _repository.SaveAsync();

            return Response<string>.Ok(string.Format(PropertyResponse.ViewSessionUserLeft,
                request.Role.GetDescription()));
        }
    }
}