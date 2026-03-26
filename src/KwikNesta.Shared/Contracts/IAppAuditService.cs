using KwikNesta.Shared.ServiceNotifications.Infra;

namespace KwikNesta.Shared.Contracts
{
    public interface IAppAuditService
    {
        Task WriteAsync(CreateAuditNotification notification);
    }
}
