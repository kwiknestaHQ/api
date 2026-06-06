using KwikNesta.Mediator.Cores.Abstractions;
using KwikNesta.Shared.Extensions;
using KwikNesta.Shared.Models.Enumerations.Property;
using KwikNesta.Shared.Responses;
using KwikNesta.Shared.ServiceDTOs.Property;
using KwikNesta.Shared.ServiceQueries.Identity;
using KwikNesta.Shared.ServiceQueries.Property;
using KwikNestaProperty.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace KwikNestaProperty.Application.Handlers
{
    public class GetVerificationRequestByIdQueryHandler(IPropertyRepositoryManager repository, 
                                                        IKNMediator mediator)
        : IKNRequestHandler<GetVerificationRequestByIdQuery, Response<VerificationRequestDto>>
    {
        private readonly IPropertyRepositoryManager _repository = repository;
        private readonly IKNMediator _mediator = mediator;

        public async Task<Response<VerificationRequestDto>> HandleAsync(GetVerificationRequestByIdQuery request, CancellationToken cancellationToken)
        {
            var verificationRequest = await _repository.OwnershipVerification
                .Get(ov => ov.Id == request.RequestId)
                .Select(ov => new VerificationRequestDto
                {
                    Id = ov.Id,
                    Status = ov.Status,
                    PropertyId = ov.PropertyId,
                    SubmittedOn = ov.CreatedOn,
                    Reason = ov.RejectionReason,
                    ReviewedOn = ov.ReviewedAt,
                    Documents = ov.Documents.Select(d => new PropertyDocumentDto
                    {
                        Id = d.Id,
                        FileUrl = d.FileUrl,
                        DocumentType = d.DocumentType,
                        UploadedAt = d.CreatedOn,
                        DocumentTypeText = d.DocumentType == EDocumentType.Other ? 
                            $"{d.DocumentType.GetDescription()} ({d.OtherDocumentType})" :
                            d.DocumentType.GetDescription()
                    }).ToList()
                }).FirstOrDefaultAsync(cancellationToken);
            if (verificationRequest == null)
            {
                return Response<VerificationRequestDto>
                    .Fail(string.Format(PropertyResponse.RecordNotFound, "Verification Request"), 404);
            }

            var property = await _repository.Property
                .Get(p => p.Id == verificationRequest.PropertyId)
                .Select(p => new PropertyLeanDto
                {
                    Id = p.Id,
                    Title = p.Title,
                    Type = p.Type.GetDescription(),
                    ListingType = p.ListingType.GetDescription(),
                    PriceFrequency = p.PriceFrequency.GetDescription(),
                    Address = p.Location.Address,
                    City = p.Location.City,
                    State = p.Location.State,
                    Country = p.Location.Country,
                    Latitude = p.Location.Latitude,
                    Longitude = p.Location.Longitude,
                    LocationVerificationStatus = p.Location.VerificationStatus.GetDescription(),
                    OwnerId = p.OwnerId
                }).FirstOrDefaultAsync(cancellationToken);
            if(property == null)
            {
                return Response<VerificationRequestDto>
                    .Fail(string.Format(PropertyResponse.RecordNotFound, "Property"), 404);
            }

            var ownerInfo = await _mediator.SendAsync(new GetUserByIdQuery 
            { 
                Id = property.OwnerId 
            }, cancellationToken);
            if (!ownerInfo.Success)
            {
                return Response<VerificationRequestDto>.Fail(ownerInfo.Message, ownerInfo.StatusCode);
            }

            verificationRequest.Property = property;
            verificationRequest.Owner = new PropertyOwnerLean
            {
                Id = ownerInfo.Data.Id,
                Email = ownerInfo.Data.Email,
                Name = $"{ownerInfo.Data.FirstName}, {ownerInfo.Data.LastName}"
            };

            verificationRequest.PreviousRequests = await _repository.OwnershipVerification
                .Get(v => v.Id != request.RequestId && 
                        v.Id == verificationRequest.PropertyId && 
                        v.CreatedOn < verificationRequest.SubmittedOn)
                .Select(v => new VerificationRequestLeanDto
                {
                    Id = v.Id,
                    Status = v.Status,
                    PropertyId = v.PropertyId,
                    SubmittedOn = v.CreatedOn,
                    Reason = v.RejectionReason,
                    ReviewedOn = v.ReviewedAt
                }).ToListAsync(cancellationToken);

            return Response<VerificationRequestDto>.Ok(verificationRequest);
        }
    }
}