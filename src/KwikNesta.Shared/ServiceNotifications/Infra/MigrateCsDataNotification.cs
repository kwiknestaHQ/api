using KwikNesta.Mediator.Cores.Abstractions;

namespace KwikNesta.Shared.ServiceNotifications.Infra
{
    public class MigrateCsDataNotification : IKNNotification
    {
        public string LoggedInUserId { get; set; } = default!;
        public string LoggedInUserEmail { get; set; } = default!;
        public string? LoggedInUserIpAddress { get; set; }
    }
}