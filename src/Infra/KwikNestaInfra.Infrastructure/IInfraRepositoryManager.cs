using KwikNesta.Shared.Contracts;
using KwikNestaInfra.Infrastructure.Contracts;

namespace KwikNestaInfra.Infrastructure
{
    public interface IInfraRepositoryManager : IBaseRepositoryManager
    {
        IAuditLogRepository AuditLog { get; }
        IKNCountryRepository Country { get; }
        IKNStateRepository State { get; }
        IKNCityRepository City { get; }
        IKNTimeZoneRepository TimeZone { get; }
        IAgoraWebhookLogsRepository AgoraWebhookLogs { get; }
        IAgoraTokenRepository AgoraToken { get; }
    }
}