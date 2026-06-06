using KwikNesta.Mediator.Cores.Abstractions;
using KwikNesta.Shared.Implementations;
using KwikNesta.Shared.Models.Enumerations.Infra;
using KwikNesta.Shared.Models.Enumerations.Property;
using KwikNesta.Shared.Responses;
using KwikNesta.Shared.ServiceCommands.Property;
using KwikNestaProperty.Application.Validations;
using KwikNestaProperty.Infrastructure;

namespace KwikNestaProperty.Application.Handlers
{
    public class UpdatePropertyBasicInfoCommandHandler(IPropertyRepositoryManager repository) 
        : IKNRequestHandler<UpdatePropertyBasicInfoCommand, Response<string>>
    {
        private readonly IPropertyRepositoryManager _repository = repository;

        public async Task<Response<string>> HandleAsync(UpdatePropertyBasicInfoCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdatePropertyBasicInfoCommandValidator().Validate(request);
            if (!validator.IsValid)
            {
                return Response<string>.Fail(validator.Errors.FirstOrDefault()?.ErrorMessage ??
                    PropertyResponse.InvalidRequest, 400);
            }

            var property = await _repository.Property
                .FirstOrDefault(p => p.Id == request.PropertyId, true);
            if (property == null)
            {
                return Response<string>.Fail(string.Format(PropertyResponse.RecordNotFound, "Property"), 404);
            }

            property.Title = request.Title;
            property.Description = request.Description;
            property.Bathrooms = request.Bathrooms;
            property.Bedrooms = request.Bedrooms;
            property.Type = request.Type;
            property.AreaSize = request.AreaSize;
            property.ListingType = request.ListingType;
            property.PriceFrequency = request.PriceFrequency;
            property.ParkingSpaces = request.ParkingSpaces;
            property.LastUpdatedOn = DateTime.UtcNow;

            if (property.Price != request.Price)
            {
                if (property.Status == EListingStatus.PendingPayment || property.Status == EListingStatus.Rented)
                {
                    throw new Exception("Price cannot be updated at this stage");
                }
                
                await _repository.PropertyPriceHistory.LogAsync(property.Id, 
                                    property.Price, 
                                    request.Price);

                property.Price = request.Price;
            }

            await _repository.SaveAsync();
            AppAudit.Write(request.UserContext.Id,
                request.UserContext.Email,
                EAuditAction.UpdatedPropertyInfo,
                EAuditDomain.Property,
                property.Id.ToString(),
                request.UserContext.IpAddress);

            return Response<string>.Ok(PropertyResponse.PropertyInfoUpdated);
        }
    }
}