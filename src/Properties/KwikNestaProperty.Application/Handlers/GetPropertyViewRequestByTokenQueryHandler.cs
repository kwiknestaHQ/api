using KwikNesta.Mediator.Cores.Abstractions;
using KwikNesta.Shared.Helpers;
using KwikNesta.Shared.Models.Settings;
using KwikNesta.Shared.Responses;
using KwikNesta.Shared.ServiceDTOs.Property;
using KwikNesta.Shared.ServiceQueries.Property;
using KwikNestaProperty.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace KwikNestaProperty.Application.Handlers
{
    public class GetPropertyViewRequestByTokenQueryHandler(IPropertyRepositotyManager repository, 
                                                        IKNMediator mediator,
                                                        IOptions<KNApplicationSettings> options) 
        : IKNRequestHandler<GetPropertyViewRequestByTokenQuery, Response<ViewRequestDto>>
    {
        private readonly IPropertyRepositotyManager _repository = repository;
        private readonly IKNMediator _mediator = mediator;
        private readonly string _secret = options.Value.Jwt.Key;

        public async Task<Response<ViewRequestDto>> HandleAsync(GetPropertyViewRequestByTokenQuery request, CancellationToken cancellationToken)
        {
            var (requestId, propertyOwnerId, error) = TokenHelper.ValidateViewRequestToken(request.Token, _secret);
            if (!string.IsNullOrWhiteSpace(error) || requestId == Guid.Empty || string.IsNullOrWhiteSpace(propertyOwnerId))
            {
                return Response<ViewRequestDto>.Fail(error, StatusCodes.Status403Forbidden);
            }

            if (requestId != request.RequestId)
            {
                return Response<ViewRequestDto>.Fail(PropertyResponse.InvalidRequestId, StatusCodes.Status403Forbidden);
            }

            return await _mediator.SendAsync(new GetViewRequestByIdQuery
            {
                Id = request.RequestId
            }, cancellationToken);
        }
    }
}