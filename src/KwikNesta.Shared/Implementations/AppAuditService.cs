using KwikNesta.Mediator.Cores.Abstractions;
using KwikNesta.Shared.Contracts;
using KwikNesta.Shared.ServiceNotifications.Infra;

namespace KwikNesta.Shared.Implementations
{
    public class AppAuditService : IAppAuditService
    {
        private readonly IKNMediator _mediator;

        public AppAuditService(IKNMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task WriteAsync(CreateAuditNotification notification)
        {
            await _mediator.PublishAsync(notification);
        }
    }
}