using KwikNesta.Mediator.Cores.Abstractions;
using KwikNesta.Shared.Models.Enumerations.Property;
using KwikNesta.Shared.Responses;
using KwikNesta.Shared.ServiceDTOs.Property;
using KwikNesta.Shared.ServiceQueries.Property;
using KwikNestaProperty.Infrastructure;

namespace KwikNestaProperty.Application.Handlers
{
    public class GetPropertyViewRequestsQueryHandler(IPropertyRepositoryManager repository) : IKNRequestHandler<GetPropertyViewRequestsQuery, PagedResponse<ViewRequestLeanDto>>
    {
        private readonly IPropertyRepositoryManager _repository = repository;

        public async Task<PagedResponse<ViewRequestLeanDto>> HandleAsync(GetPropertyViewRequestsQuery request, CancellationToken cancellationToken)
        {
            return await _repository.ViewingRequest
                .Get(v => v.PropertyId == request.PropertyId &&
                    v.PaymentStatus == EViewingPaymentStatus.Paid &&
                    v.Property.OwnerId == request.UserId)
                .OrderBy(v => v.CreatedOn)
                .Select(v => new ViewRequestLeanDto
                {
                    Id = v.Id,
                    Fee = v.Fee,
                    Status = v.Status,
                    RespondedAt = v.RespondedAt,
                    Note = v.Note,
                    PaymentStatus = v.PaymentStatus,
                    RequestedDate = v.RequestedDate,
                    Type = v.Type
                }).PaginateAsync(request.Page, request.PageSize, cancellationToken);
        }
    }
}
