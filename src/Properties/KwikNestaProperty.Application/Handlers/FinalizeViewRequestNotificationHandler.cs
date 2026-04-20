using KwikNesta.Mediator.Cores.Abstractions;
using KwikNesta.Shared.Extensions;
using KwikNesta.Shared.Helpers;
using KwikNesta.Shared.Implementations;
using KwikNesta.Shared.Models.Enumerations.Infra;
using KwikNesta.Shared.Models.Enumerations.Payments;
using KwikNesta.Shared.Models.Enumerations.Property;
using KwikNesta.Shared.Models.Settings;
using KwikNesta.Shared.Responses;
using KwikNesta.Shared.ServiceCommands.Property;
using KwikNesta.Shared.ServiceQueries.Identity;
using KwikNesta.Shared.ServiceQueries.Payment;
using KwikNesta.Shared.ServiceQueries.Property;
using KwikNestaProperty.Infrastructure;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace KwikNestaProperty.Application.Handlers
{
    public class FinalizeViewRequestNotificationHandler(IPropertyRepositotyManager repository, 
                                                IHostEnvironment host,
                                                IKNMediator mediator,
                                                IOptions<KNApplicationSettings> options,
                                                ILogger<FinalizeViewRequestNotificationHandler> logger) 
        : IKNNotificationHandler<FinalizeViewRequestNotification>
    {
        private readonly IPropertyRepositotyManager _repository = repository;
        private readonly IHostEnvironment _host = host;
        private readonly IKNMediator _mediator = mediator;
        private readonly ILogger<FinalizeViewRequestNotificationHandler> _logger = logger;
        private const int TokenExpiryHours = 24;
        private readonly string Secret = options.Value.Jwt.Key;
        private readonly KNAdminSettings _adminSettings = options.Value.AppAdmin;

        public async Task HandleAsync(FinalizeViewRequestNotification notification, CancellationToken cancellationToken)
        {
            if(string.IsNullOrEmpty(notification.Reference))
            {
                _logger.LogError("Invalid request: {notification}", notification);
                return;
            }

            var paymentResponse = await _mediator.SendAsync(new GetPaymentByReferenceQuery
            {
                Reference = notification.Reference,
            }, cancellationToken);

            if(!paymentResponse.Success)
            {
                _logger.LogError("Payment Record not found for reference {Reference}", notification.Reference);
                return;
            }

            var payment = paymentResponse.Data;
            var viewRequest = await _repository.ViewingRequest
                .FirstOrDefault(vr => vr.Id == payment.ReferenceId, true);
            if(viewRequest == null)
            {
                _logger.LogError("View Request not found with this Id: {RequestId}", payment.ReferenceId);
                return;
            }

            if (payment.Status != EPaymentStatus.Successful)
            {
                if(payment.Status == EPaymentStatus.Failed)
                {
                    viewRequest.PaymentStatus = EViewingPaymentStatus.Failed;
                    await _repository.SaveAsync();
                }

                _logger.LogWarning("Payment Status not Successful");
                return;
            }

            var propertyResponse = await _mediator.SendAsync(new GetPropertyLeanQuery
            {
                Id = viewRequest.PropertyId,
            }, cancellationToken);

            if (!propertyResponse.Success)
            {
                _logger.LogError(propertyResponse.Message);
                return;
            }

            viewRequest.PaymentStatus = EViewingPaymentStatus.Paid;
            viewRequest.LastUpdatedOn = DateTime.UtcNow;
            await _repository.SaveAsync();

            var property = propertyResponse.Data;
            var token = TokenHelper.GenerateViewRequestToken(viewRequest.Id, Secret, TokenExpiryHours);
            var viewingLink = UrlHelpers.GetViewRequestResponseLink(_adminSettings.BaseUrl, viewRequest.Id, token);

            Notifications.SendEmail(property.OwnerEmail,
                PropertyResponse.ViewRequestSubject,
                _host.GetViewRequestResponseNotification(property.OwnerFirstName,
                        property.Title,
                        _adminSettings.SupportEmail,
                        viewingLink,
                        property.Address,
                        viewRequest.RequestedDate,
                        viewRequest.Type));

            var userResponse = await _mediator.SendAsync(new GetUserByIdQuery
            {
                Id = viewRequest.UserId
            });

            if (userResponse.Success)
            {
                AppAudit.Write(userResponse.Data.Id,
                    userResponse.Data.Email,
                    EAuditAction.ViewRequested,
                    EAuditDomain.ViewRequest,
                    viewRequest.Id.ToString(),
                    string.Empty);
            }
        }
    }
}