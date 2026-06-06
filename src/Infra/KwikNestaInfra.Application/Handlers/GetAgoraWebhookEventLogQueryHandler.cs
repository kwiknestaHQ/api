using KwikNesta.Mediator.Cores.Abstractions;
using KwikNesta.Shared.Responses;
using KwikNesta.Shared.ServiceDTOs.Infra;
using KwikNesta.Shared.ServiceQueries.Infra;
using KwikNestaInfra.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace KwikNestaInfra.Application.Handlers
{
    public class GetAgoraWebhookEventLogQueryHandler(IInfraRepositoryManager repository)
        : IKNRequestHandler<GetAgoraWebhookEventLogQuery, Response<List<AgoraEventLogDto>>>
    {
        private readonly IInfraRepositoryManager _repository = repository;

        public async Task<Response<List<AgoraEventLogDto>>> HandleAsync(GetAgoraWebhookEventLogQuery request, CancellationToken cancellationToken)
        {
            var eventsQuery = _repository.AgoraWebhookLogs
                .Get(e => e.Channel == request.Channel);

            if (request.Event.HasValue)
            {
                eventsQuery = eventsQuery
                    .Where(e => e.Type == request.Event.Value);
            }

            return Response<List<AgoraEventLogDto>>.Ok(await eventsQuery
                .OrderByDescending(e => e.CreatedAt)
                .Select(e => new AgoraEventLogDto
                {
                    Channel = e.Channel,
                    Module = e.Module,
                    Timestamp = e.Timestamp,
                    Type = e.Type,
                    UId = e.UId
                }).ToListAsync(cancellationToken));
        }
    }
}