using KwikNestaInfra.Infrastructure.Contracts;
using KwikNestaInfra.Infrastructure.Data;
using KwikNestaInfra.Infrastructure.Repositories;

namespace KwikNestaInfra.Infrastructure
{
    public class InfraRepositoryManager(InfraServiceDbContext context) : IInfraRepositoryManager
    {
        private readonly Lazy<IAuditLogRepository> _auditLogRepository =
            new(() => new AuditLogRepository(context));
        private readonly Lazy<IKNCountryRepository> _kNCountryRepository = 
            new(() => new KNCountryRepository(context));
        private readonly Lazy<IKNStateRepository> _kNStateRepository =
           new(() => new KNStateRepository(context));
        private readonly Lazy<IKNCityRepository> _kNCityRepository =
           new(() => new KNCityRepository(context));
        private readonly Lazy<IKNTimeZoneRepository> _kNTimeZoneRepository =
           new(() => new KNTimeZoneRepository(context));

        private readonly InfraServiceDbContext _context = context;

        public IAuditLogRepository AuditLog => _auditLogRepository.Value;
        public IKNCountryRepository Country => _kNCountryRepository.Value;
        public IKNStateRepository State => _kNStateRepository.Value;
        public IKNCityRepository City => _kNCityRepository.Value;
        public IKNTimeZoneRepository TimeZone => _kNTimeZoneRepository.Value;
        public async Task BeginTransaction(Func<Task> action)
        {
            await using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                await action();

                await SaveAsync();
                await transaction.CommitAsync();

            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}