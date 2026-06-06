using KwikNesta.Mediator.Cores.Abstractions;
using KwikNesta.Shared.Responses;
using KwikNesta.Shared.ServiceCommands.Infra;
using KwikNestaInfra.Application.Validators;
using KwikNestaInfra.Domain.Entities;
using KwikNestaInfra.Infrastructure;
using Microsoft.AspNetCore.Http;

namespace KwikNestaInfra.Application.Handlers
{
    public class CreateAgoraTokenCommandHandler(IInfraRepositoryManager repository)
        : IKNRequestHandler<CreateAgoraTokenCommand, Response<string>>
    {
        private readonly IInfraRepositoryManager _repository = repository;

        public async Task<Response<string>> HandleAsync(CreateAgoraTokenCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateAgoraTokenCommandValidator().Validate(request);

            if (!validator.IsValid)
            {
                return Response<string>.Fail(validator.Errors.FirstOrDefault()?.ErrorMessage ??
                    InfraResponses.InvalidRequest, StatusCodes.Status400BadRequest);
            }

            await _repository.AgoraToken.AddAsync(new AgoraToken
            {
                ChannelName = request.Channel,
                Account = request.UserId,
                Token = request.Token,
                ExpiresAt = request.Expires
            });

            await _repository.SaveAsync();
            return Response<string>.Ok("Agora token successfully saved.");
        }
    }
}