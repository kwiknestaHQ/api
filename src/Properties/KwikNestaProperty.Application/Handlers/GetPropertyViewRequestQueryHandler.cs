using KwikNesta.Mediator.Cores.Abstractions;
using KwikNesta.Shared.Models.Enumerations.Property;
using KwikNesta.Shared.Responses;
using KwikNesta.Shared.ServiceDTOs.Property;
using KwikNesta.Shared.ServiceQueries.Identity;
using KwikNesta.Shared.ServiceQueries.Property;
using KwikNestaProperty.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace KwikNestaProperty.Application.Handlers
{
    internal class GetPropertyViewRequestQueryHandler(IPropertyRepositotyManager repository, 
                                                    IKNMediator mediator) 
        : IKNRequestHandler<GetPropertyViewRequestQuery, Response<ViewRequestDto>>
    {
        private readonly IPropertyRepositotyManager _repository = repository;
        private readonly IKNMediator _mediator = mediator;

        public async Task<Response<ViewRequestDto>> HandleAsync(GetPropertyViewRequestQuery request, CancellationToken cancellationToken)
        {
            var viewRequest = await _repository.ViewingRequest
                .FirstOrDefault(v => v.Id == request.RequestId && 
                                v.PropertyId == request.PropertyId && 
                                v.Property.OwnerId == request.UserId);
            if (viewRequest == null)
            {
                return Response<ViewRequestDto>.Fail(string.Format(PropertyResponse.RecordNotFound, "View Request"),
                    StatusCodes.Status404NotFound);
            }

            var property = await _repository.Property
                .Get(p => p.Id == viewRequest.PropertyId)
                .Select(p => new ViewRequestPropertyDto
                {
                    Id = p.Id,
                    Title = p.Title,
                    Price = p.Price,
                    Location = $"{p.Location.Address}, {p.Location.City}, {p.Location.State}",
                    Image = p.Media.Where(i => i.IsPrimary && i.Type == EMediaType.Image)
                        .Select(i => i.Url)
                        .FirstOrDefault()
                }).FirstOrDefaultAsync(cancellationToken);

            if (property == null)
            {
                return Response<ViewRequestDto>.Fail(string.Format(PropertyResponse.RecordNotFound, "Property"),
                    StatusCodes.Status404NotFound);
            }

            var userResult = await _mediator.SendAsync(new LoggedInUserQuery
            {
                UserId = viewRequest.UserId,
            });

            if (!userResult.Success)
            {
                return Response<ViewRequestDto>.Fail(userResult.Message, userResult.StatusCode);
            }

            var user = userResult.Data;
            return Response<ViewRequestDto>.Ok(new ViewRequestDto
            {
                Id = viewRequest.Id,
                Fee = viewRequest.Fee,
                Status = viewRequest.Status,
                Type = viewRequest.Type,
                SessionId = viewRequest.SessionId,
                PaymentStatus = viewRequest.PaymentStatus,
                RespondedAt = viewRequest.RespondedAt,
                RequestedDate = viewRequest.RequestedDate,
                Note = viewRequest.Note,
                CreateAt = viewRequest.CreatedOn,
                Property = property,
                Requester = new ViewRequesterDto
                {
                    Id = user.Id,
                    Email = user.Email,
                    Name = $"{user.FirstName} {user.LastName}",
                    Phone = user.PhoneNumber
                }
            });
        }
    }
}