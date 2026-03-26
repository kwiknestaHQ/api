using KwikNestaIdentity.Infrastructure.Contracts;

namespace KwikNestaIdentity.Infrastructure
{
    public interface IIdentityRepositoryManager
    {
        IOtpEntryRepository OtpEntry { get; }
        IRefreshTokenRepository RefreshToken { get; }

        Task BeginTransaction(Func<Task> action);
        Task SaveAsync();
    }
}