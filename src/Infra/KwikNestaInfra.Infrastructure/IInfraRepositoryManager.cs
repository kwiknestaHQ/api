using KwikNestaInfra.Infrastructure.Contracts;

namespace KwikNestaInfra.Infrastructure
{
    public interface IInfraRepositoryManager
    {
        IAuditLogRepository AuditLog { get; }
        IKNCountryRepository Country { get; }
        IKNStateRepository State { get; }
        IKNCityRepository City { get; }
        IKNTimeZoneRepository TimeZone { get; }

        Task BeginTransaction(Func<Task> action);
        Task SaveAsync();
    }
}