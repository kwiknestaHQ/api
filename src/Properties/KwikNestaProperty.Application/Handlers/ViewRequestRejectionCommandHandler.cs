using Hangfire;
using Hangfire.Console;
using Hangfire.Server;
using KwikNesta.Mediator.Cores.Abstractions;
using KwikNesta.Shared.Extensions;
using KwikNesta.Shared.Helpers;
using KwikNesta.Shared.Implementations;
using KwikNesta.Shared.Models.Enumerations.Infra;
using KwikNesta.Shared.Models.Enumerations.Property;
using KwikNesta.Shared.Models.Settings;
using KwikNesta.Shared.Responses;
using KwikNesta.Shared.ServiceCommands.Payment;
using KwikNesta.Shared.ServiceCommands.Property;
using KwikNesta.Shared.ServiceDTOs.Identity;
using KwikNesta.Shared.ServiceDTOs.Payment;
using KwikNesta.Shared.ServiceQueries.Identity;
using KwikNesta.Shared.ServiceQueries.Payment;
using KwikNestaProperty.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace KwikNestaProperty.Application.Handlers
{
    public class ViewRequestRejectionCommandHandler(IPropertyRepositotyManager repository, 
                                        IKNMediator mediator,
                                        IOptions<KNApplicationSettings> options,
                                        IHostEnvironment host) 
        : IKNRequestHandler<ViewRequestRejectionCommand, Response<string>>
    {
        private readonly IPropertyRepositotyManager _repository = repository;
        private readonly IKNMediator _mediator = mediator;
        private readonly IOptions<KNApplicationSettings> options = options;
        private readonly IHostEnvironment _host = host;
        private readonly JwtSettings _jwtSettings = options.Value.Jwt ?? 
            throw new ArgumentNullException(nameof(JwtSettings));

        public async Task<Response<string>> HandleAsync(ViewRequestRejectionCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.Token))
            {
                return Response<string>.Fail("Not authenticated", StatusCodes.Status403Forbidden);
            }

            var (requestId, propertyOwnerId, error) = TokenHelper.ValidateViewRequestToken(request.Token!, _jwtSettings.Key);
            var loggedInUserId = !string.IsNullOrWhiteSpace(request.Context.Id) ?
                request.Context.Id : propertyOwnerId;

            var landlordResult = await _mediator.SendAsync(new GetUserByIdQuery
            {
                Id = loggedInUserId
            }, cancellationToken);

            if (!landlordResult.Success)
            {
                return Response<string>.Fail(landlordResult.Message, landlordResult.StatusCode);
            }

            var landlord = landlordResult.Data;
            var viewRequest = await _repository.ViewingRequest
                .FirstOrDefault(v => v.Id == request.RequestId && v.Property.OwnerId == landlord.Id, true);

            if (viewRequest == null)
            {
                return Response<string>.Fail(string.Format(PropertyResponse.RecordNotFound, "View Request"),
                    StatusCodes.Status404NotFound);
            }

            if (viewRequest.Status == EViewingStatus.Rejected)
            {
                return Response<string>.Fail(PropertyResponse.ViewRequestAlreadyRejected,
                   StatusCodes.Status409Conflict);
            }

            var requesterResult = await _mediator.SendAsync(new GetUserByIdQuery
            {
                Id = viewRequest.UserId
            }, cancellationToken);

            if (!requesterResult.Success || requesterResult.Data == null)
            {
                return Response<string>.Fail(requesterResult.Message, requesterResult.StatusCode);
            }

            var paymentResponse = await _mediator.SendAsync(new GetPaymentByReferenceIdQuery
            {
                ReferenceId = viewRequest.Id,
            }, cancellationToken);

            if (!paymentResponse.Success)
            {
                return Response<string>.Fail(paymentResponse.Message, paymentResponse.StatusCode);
            }

            viewRequest.Status = EViewingStatus.Rejected;
            viewRequest.LastUpdatedOn = DateTime.UtcNow;
            await _repository.SaveAsync();

            BackgroundJob.Enqueue(() 
                => InitiateRefundAndNotifyUser(requesterResult.Data, 
                        paymentResponse.Data, 
                        viewRequest.PropertyId, 
                        viewRequest.RequestedDate, 
                        null!));

            AppAudit.Write(landlord.Id,
                landlord.Email,
                EAuditAction.ViewRequestApproved,
                EAuditDomain.ViewRequest,
                viewRequest.Id.ToString(),
                request.Context.IpAddress);

            return Response<string>.Ok(PropertyResponse.ViewRequestRejected);
        }

        public async Task InitiateRefundAndNotifyUser(CurrentUserDto requester, 
                                        PaymentDto payment, 
                                        Guid propertyId, 
                                        DateTime requestedDate, 
                                        PerformContext context)
        {
            context.WriteLine("[InitiateRefundAndNotifyUser] Running process");
           
            var refundRequest = await _mediator.SendAsync(new InitiatePaystackRefundCommand
            {
                UserEmail = requester.Email,
                UserId = requester.Id,
                Amount = payment.Amount,
                PaymentReference = payment.Reference
            });

            if (!refundRequest.Success)
            {
                context.WriteLine($"[InitiateRefundAndNotifyUser] An error occurred: {refundRequest.Message}");
                return;
            }

            var property = await _repository.Property
                .Get(p => p.Id == propertyId)
                .Select(p => new
                {
                    p.Title,
                    PropertyAddress = p.Location.Address,
                }).FirstOrDefaultAsync();

            if(property == null)
            {
                context.WriteLine($"[InitiateRefundAndNotifyUser] Property not found with Id: {propertyId}");
                return;
            }

            context.WriteLine($"[InitiateRefundAndNotifyUser] Refund completed for Id: {refundRequest.Data.Id}");

            Notifications.SendEmail(requester.Email, PropertyResponse.PropertyViewRequestDeclinedSubject,
              _host.GetInformationalNotification(requester.FirstName,
                                      string.Format(PropertyResponse.PropertyViewRequestDeclinedMessage, 
                                            property.Title, 
                                            property.PropertyAddress, 
                                            requestedDate.FormatAsWat())));


            context.WriteLine("[InitiateRefundAndNotifyUser] Completed process");
        }
    }
}