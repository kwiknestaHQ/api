using KwikNesta.Shared.Contracts;
using KwikNestaIdentity.Infrastructure.Contracts;

namespace KwikNestaIdentity.Infrastructure
{
    public interface IIdentityRepositoryManager : IBaseRepositoryManager
    {
        IOtpEntryRepository OtpEntry { get; }
        IRefreshTokenRepository RefreshToken { get; }
    }
}