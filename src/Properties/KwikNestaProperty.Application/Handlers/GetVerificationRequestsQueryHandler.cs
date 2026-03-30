using KwikNesta.Mediator.Cores.Abstractions;
using KwikNesta.Shared.Responses;
using KwikNesta.Shared.ServiceDTOs.Property;
using KwikNesta.Shared.ServiceQueries.Property;
using KwikNestaProperty.Infrastructure;

namespace KwikNestaProperty.Application.Handlers
{
    public class GetVerificationRequestsQueryHandler(IPropertyRepositotyManager repository) 
        : IKNRequestHandler<GetVerificationRequestsQuery, PagedResponse<VerificationRequestLeanDto>>
    {
        private readonly IPropertyRepositotyManager _repository = repository;

        public async Task<PagedResponse<VerificationRequestLeanDto>> HandleAsync(GetVerificationRequestsQuery request, CancellationToken cancellationToken)
        {
            var data = _repository.OwnershipVerification
                .Get(or => or.Status == request.Status)
                .OrderByDescending(or => or.CreatedOn)
                .Select(or => new VerificationRequestLeanDto
                {
                    Id = or.Id,
                    PropertyId = or.PropertyId,
                    Status = or.Status,
                    SubmittedOn = or.CreatedOn,
                    Reason = or.RejectionReason,
                    ReviewedOn = or.ReviewedAt
                }).Paginate(request.Page, request.PageSize);

            return await Task.FromResult(data);
        }
    }
}