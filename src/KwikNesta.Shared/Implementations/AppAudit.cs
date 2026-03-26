using Hangfire;
using KwikNesta.Shared.Contracts;
using KwikNesta.Shared.Models.Enumerations.Infra;
using KwikNesta.Shared.ServiceNotifications.Infra;

namespace KwikNesta.Shared.Implementations
{
    public class AppAudit
    {
        public static void Write(string userId,
                                string userName,
                                EAuditAction action,
                                EAuditDomain domain,
                                string domainId,
                                string? ip,
                                string? desc = null)
        {
            BackgroundJob.Enqueue<IAppAuditService>(x =>
                                x.WriteAsync(new CreateAuditNotification
                                {
                                    UserId = userId,
                                    UserName = userName,
                                    Action = action,
                                    Domain = domain,
                                    DomainId = domainId,
                                    IpAddress = ip,
                                    Description = desc
                                }));
        }
    }
}
