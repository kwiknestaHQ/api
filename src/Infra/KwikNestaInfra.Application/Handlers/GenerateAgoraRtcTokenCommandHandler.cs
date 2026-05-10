using AgoraIO.Media;
using KwikNesta.Mediator.Cores.Abstractions;
using KwikNesta.Shared.Models.Settings;
using KwikNesta.Shared.Responses;
using KwikNesta.Shared.ServiceCommands.Infra;
using KwikNesta.Shared.ServiceDTOs.Infra;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace KwikNestaInfra.Application.Handlers
{
    public class GenerateAgoraRtcTokenCommandHandler(IOptions<KNApplicationSettings> options) 
        : IKNRequestHandler<GenerateAgoraRtcTokenCommand, Response<AgoraRtcTokenResult>>
    {
        private readonly AgoraSetting _agora = options.Value.Agora ?? 
            throw new ArgumentNullException(nameof(AgoraSetting));

        public async Task<Response<AgoraRtcTokenResult>> HandleAsync(GenerateAgoraRtcTokenCommand request, CancellationToken cancellationToken)
        {
            if(string.IsNullOrWhiteSpace(request.ChannelName) || request.UId <= default(uint))
            {
                return Response<AgoraRtcTokenResult>.Fail(InfraResponses.InvalidRequest,
                    StatusCodes.Status400BadRequest);
            }

            uint expireTimestamp = (uint)(DateTimeOffset.UtcNow.ToUnixTimeSeconds() + _agora.ExpiryInSeconds);
            var role = request.IsPublisher ? RtcTokenBuilder.Role.RolePublisher : RtcTokenBuilder.Role.RoleSubscriber;

            var token = RtcTokenBuilder.buildTokenWithUID(
                    appID: _agora.AppId,
                    appCertificate: _agora.AppCert,
                    channelName: request.ChannelName,
                    uid: request.UId,
                    role: role,
                    privilegeExpiredTs: expireTimestamp);

            await Task.CompletedTask;
            return Response<AgoraRtcTokenResult>.Ok(new AgoraRtcTokenResult(token));
        }
    }
}