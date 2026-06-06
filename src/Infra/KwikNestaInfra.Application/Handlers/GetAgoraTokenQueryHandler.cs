using KwikNesta.Mediator.Cores.Abstractions;
using KwikNesta.Shared.Responses;
using KwikNesta.Shared.ServiceDTOs.Infra;
using KwikNesta.Shared.ServiceQueries.Infra;
using KwikNestaInfra.Infrastructure;
using Microsoft.AspNetCore.Http;

namespace KwikNestaInfra.Application.Handlers
{
    public class GetAgoraTokenQueryHandler(IInfraRepositoryManager repository) 
        : IKNRequestHandler<GetAgoraTokenQuery, Response<AgoraTokenDto>>
    {
        private readonly IInfraRepositoryManager _repository = repository;

        public async Task<Response<AgoraTokenDto>> HandleAsync(GetAgoraTokenQuery request, CancellationToken cancellationToken)
        {
            var existing = await _repository.AgoraToken
                .FirstOrDefault(t => t.ChannelName == request.Channel
                               && t.Account == request.UserId);

            if(existing == null)
            {
                return Response<AgoraTokenDto>.Fail(string.Format(InfraResponses.RecordNotFound, 
                    "Agora Token"),
                    StatusCodes.Status404NotFound);
            }

            if(DateTime.UtcNow >= existing.ExpiresAt)
            {
                return Response<AgoraTokenDto>.Fail(InfraResponses.TokenExpired,
                    StatusCodes.Status412PreconditionFailed);
            }

            return Response<AgoraTokenDto>.Ok(new AgoraTokenDto
            {
                Account = existing.Account,
                ExpiresAt = existing.ExpiresAt,
                ChannelName = existing.ChannelName,
                Token = existing.Token
            });
        }
    }
}