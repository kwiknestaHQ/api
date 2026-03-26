using KwikNestaIdentity.Infrastructure.Contracts;
using KwikNestaIdentity.Infrastructure.Data;
using KwikNestaIdentity.Infrastructure.Repositories;

namespace KwikNestaIdentity.Infrastructure
{
    public class IdentityRepositoryManager(IdentityServiceDbContext context) : IIdentityRepositoryManager
    {
        private readonly Lazy<IOtpEntryRepository> _entryRepository = 
            new Lazy<IOtpEntryRepository>(() => new OtpEntryRepository(context));
        private readonly Lazy<IRefreshTokenRepository> _refreshTokenRepository = 
            new Lazy<IRefreshTokenRepository>(() => new RefreshTokenRepository(context));

        private readonly IdentityServiceDbContext _context = context;

        public IOtpEntryRepository OtpEntry => _entryRepository.Value;
        public IRefreshTokenRepository RefreshToken => _refreshTokenRepository.Value;

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