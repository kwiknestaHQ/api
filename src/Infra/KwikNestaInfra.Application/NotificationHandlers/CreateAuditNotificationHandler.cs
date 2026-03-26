using KwikNesta.Mediator.Cores.Abstractions;
using KwikNesta.Shared.Extensions;
using KwikNesta.Shared.ServiceNotifications.Infra;
using KwikNestaInfra.Application.Validators;
using KwikNestaInfra.Domain.Entities;
using KwikNestaInfra.Infrastructure;
using Microsoft.Extensions.Logging;

namespace KwikNestaInfra.Application.NotificationHandlers
{
    public class CreateAuditNotificationHandler : IKNNotificationHandler<CreateAuditNotification>
    {
        private readonly IInfraRepositoryManager _repository;
        private readonly ILogger<CreateAuditNotificationHandler> _logger;

        public CreateAuditNotificationHandler(IInfraRepositoryManager repository,
                                            ILogger<CreateAuditNotificationHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task HandleAsync(CreateAuditNotification notification, CancellationToken cancellationToken)
        {
            if (notification == null)
            {
                _logger.LogError("Audit Notification or Audit object is null");
                return;
            }

            var validator = new CreateAuditNotificationValidator()
                .Validate(notification);

            if (!validator.IsValid)
            {
                _logger.LogError($"Invalid Audit Notification request: {string.Join(',', validator.Errors)}");
                return;
            }

            await _repository.AuditLog.AddAsync(new AuditLog
            {
                UserName = notification.UserName,
                UserId = notification.UserId,
                DomainId = notification.DomainId,
                Domain = notification.Domain,
                Action = notification.Action,
                IpAddress = notification.IpAddress,
                Description = notification.Description
            });

            await _repository.SaveAsync();
            _logger.LogInformation("Audit trail successfully added. Action Performed: {0}", notification.Action.GetDescription());
        }
    }
}