using KwikNesta.Mediator.Cores.Abstractions;
using KwikNesta.Shared.Responses;
using KwikNesta.Shared.ServiceDTOs.Property;
using KwikNesta.Shared.ServiceQueries.Identity;
using KwikNesta.Shared.ServiceQueries.Property;
using KwikNestaProperty.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace KwikNestaProperty.Application.Handlers
{
    public class GetPropertyLeanQueryHandler(IPropertyRepositoryManager repository, 
                                            IKNMediator mediator) 
        : IKNRequestHandler<GetPropertyLeanQuery, Response<PropertyVeryLeanDto>>
    {
        private readonly IPropertyRepositoryManager _repository = repository;
        private readonly IKNMediator _mediator = mediator;

        public async Task<Response<PropertyVeryLeanDto>> HandleAsync(GetPropertyLeanQuery request, CancellationToken cancellationToken)
        {
            var property = await _repository.Property
                .Get(p => p.Id == request.Id)
                .Select(p => new PropertyVeryLeanDto
                {
                    Id = p.Id,
                    Title = p.Title,
                    Address = p.Location.Address,
                    Price = p.Price,
                    OwnerId = p.OwnerId
                }).FirstOrDefaultAsync(cancellationToken);

            if (property == null)
            {
                return Response<PropertyVeryLeanDto>.Fail(string.Format(PropertyResponse.RecordNotFound, "Property"),
                    StatusCodes.Status404NotFound);
            }

            var ownerInfo = await _mediator.SendAsync(new GetUserByIdQuery
            {
                Id = property.OwnerId
            }, cancellationToken);
            if (!ownerInfo.Success)
            {
                return Response<PropertyVeryLeanDto>.Fail(ownerInfo.Message, ownerInfo.StatusCode);
            }

            property.OwnerFirstName = ownerInfo.Data.FirstName;
            property.OwnerEmail = ownerInfo.Data.Email;
            return Response<PropertyVeryLeanDto>.Ok(property);
        }
    }
}
