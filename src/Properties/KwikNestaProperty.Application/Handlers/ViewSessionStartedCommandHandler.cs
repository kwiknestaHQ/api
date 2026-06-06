using KwikNesta.Mediator.Cores.Abstractions;
using KwikNesta.Shared.Responses;
using KwikNesta.Shared.ServiceCommands.Property;
using KwikNestaProperty.Infrastructure;
using Microsoft.AspNetCore.Http;

namespace KwikNestaProperty.Application.Handlers
{
    public class ViewSessionStartedCommandHandler(IPropertyRepositoryManager repository) : IKNRequestHandler<ViewSessionStartedCommand, Response<string>>
    {
        private readonly IPropertyRepositoryManager _repository = repository;

        public async Task<Response<string>> HandleAsync(ViewSessionStartedCommand request, CancellationToken cancellationToken)
        {
            var session = await _repository.ViewingSession
                .FirstOrDefault(vs => vs.ChannelName == request.ChannelName, true);
            if (session == null)
            {
                return Response<string>.Fail(string.Format(PropertyResponse.RecordNotFound, "Viewing Session"),
                    StatusCodes.Status404NotFound);
            }

            session.MarkAsInProgress(request.Timestamp);
            await _repository.SaveAsync();

            return Response<string>.Ok(PropertyResponse.SessionMarkedInProgress);
        }
    }
}