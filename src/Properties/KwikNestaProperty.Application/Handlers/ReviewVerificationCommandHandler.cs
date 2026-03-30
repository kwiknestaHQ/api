using KwikNesta.Mediator.Cores.Abstractions;
using KwikNesta.Shared.Extensions;
using KwikNesta.Shared.Helpers;
using KwikNesta.Shared.Implementations;
using KwikNesta.Shared.Models.Enumerations.Infra;
using KwikNesta.Shared.Models.Enumerations.Property;
using KwikNesta.Shared.Models.Settings;
using KwikNesta.Shared.Responses;
using KwikNesta.Shared.ServiceCommands.Property;
using KwikNesta.Shared.ServiceDTOs.Identity;
using KwikNesta.Shared.ServiceQueries.Identity;
using KwikNestaProperty.Domain.Entities;
using KwikNestaProperty.Infrastructure;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace KwikNestaProperty.Application.Handlers
{
    public class ReviewVerificationCommandHandler(IPropertyRepositotyManager repository, 
                                                IKNMediator mediator, 
                                                IHostEnvironment host,
                                                IOptions<KNApplicationSettings> options) 
        : IKNRequestHandler<ReviewVerificationCommand, Response<string>>
    {
        private readonly IPropertyRepositotyManager _repository = repository;
        private readonly IKNMediator _mediator = mediator;
        private readonly IHostEnvironment _host = host;
        private readonly string _supportEmail = options.Value.AppAdmin.SupportEmail;

        public async Task<Response<string>> HandleAsync(ReviewVerificationCommand request, CancellationToken cancellationToken)
        {
            var validation = await Validate(request);
            if (!validation.Success)
            {
                return Response<string>.Fail(validation.Message, validation.StatusCode);
            }

            var verificationRequest = validation.Data.Request;
            var property = validation.Data.Prop;

            verificationRequest.Status = request.Action == EVerificationAction.Approve
                    ? EVerificationStatus.Approved
                    : EVerificationStatus.Declined;

            verificationRequest.RejectionReason = request.Action == EVerificationAction.Reject
                ? request.Reason
                : null;

            verificationRequest.ReviewedAt = DateTime.UtcNow;
            verificationRequest.LastUpdatedOn = DateTime.UtcNow;

            property.Status = request.Action == EVerificationAction.Approve ?
                EListingStatus.Available : EListingStatus.VerificationFailed;
            property.LastUpdatedOn = DateTime.UtcNow;

            _repository.OwnershipVerification.Update(verificationRequest);
            _repository.Property.Update(property);
            await _repository.SaveAsync();

            var owner = validation.Data.Owner;
            var subject = string.Format(PropertyResponse.PropertyVerificationInformationSubject, verificationRequest.Status.GetDescription());
            var message = request.Action == EVerificationAction.Approve ? 
                string.Format(PropertyResponse.PropertyApprovalInformationMessage, property.Title) : 
                string.Format(PropertyResponse.PropertyDeclineInformationMessage, request.Reason);

            Notifications.SendEmail(owner.Email, subject,
                _host.GetInformationalNotification(owner.FirstName,
                        message,
                        _supportEmail));

            AppAudit.Write(request.UserContext.Id,
                        request.UserContext.Email,
                        EAuditAction.PropertyVerificationReviewed,
                        EAuditDomain.Property,
                        verificationRequest.PropertyId.ToString(),
                        request.UserContext.IpAddress,
                        verificationRequest.Status.GetDescription());

            return Response<string>.Ok(PropertyResponse.VerificationRequestReviewed);
        }

        private async Task<Response<(OwnershipVerificationRequest Request, CurrentUserDto Owner, KNProperty Prop)>> Validate(ReviewVerificationCommand request)
        {
            if (request.PropertyId == Guid.Empty)
            {
                return Response<(OwnershipVerificationRequest Request, CurrentUserDto Owner, KNProperty Prop)>
                    .Fail(PropertyResponse.InvalidRequest, 400);
            }

            if (!ValidationHelper.ValidUserContext(request.UserContext))
            {
                return Response<(OwnershipVerificationRequest Request, CurrentUserDto Owner, KNProperty Prop)>
                    .Fail(PropertyResponse.UserNotAuthenticated, 403);
            }

            var property = await _repository.Property.FirstOrDefault(p => p.Id == request.PropertyId);
            if (property == null)
            {
                return Response<(OwnershipVerificationRequest Request, CurrentUserDto Owner, KNProperty Prop)>
                    .Fail(string.Format(PropertyResponse.RecordNotFound, "Property"), 404);
            }

            var owner = await _mediator.SendAsync(new GetUserByIdQuery
            {
                Id = property.OwnerId
            });

            if (!owner.Success)
            {
                return Response<(OwnershipVerificationRequest Request, CurrentUserDto Owner, KNProperty Prop)>
                    .Fail(string.Format(PropertyResponse.RecordNotFound, "Property Owner"), 404);
            }

            var verificationRequest = await _repository.OwnershipVerification
                .FirstOrDefault(x => x.Id == request.RequestId);

            if (verificationRequest == null)
            {
                return Response<(OwnershipVerificationRequest Request, CurrentUserDto Owner, KNProperty Prop)>
                    .Fail(string.Format(PropertyResponse.RecordNotFound, "Property Verification Request"), 404);
            }

            if (verificationRequest.Status != EVerificationStatus.Pending)
            {
                return Response<(OwnershipVerificationRequest Request, CurrentUserDto Owner, KNProperty Prop)>
                    .Fail(PropertyResponse.RequestProcessed, 403);
            }

            if (request.Action == EVerificationAction.Reject && string.IsNullOrWhiteSpace(request.Reason))
            {
                return Response<(OwnershipVerificationRequest Request, CurrentUserDto Owner, KNProperty Prop)>
                    .Fail(PropertyResponse.RejectionReasonRequired, 403);
            }

            return Response<(OwnershipVerificationRequest Request, CurrentUserDto Owner, KNProperty Prop)>
                .Ok((verificationRequest, owner.Data, property));
        }
    }
}