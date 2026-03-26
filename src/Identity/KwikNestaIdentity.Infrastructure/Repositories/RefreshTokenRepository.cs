using KwikNesta.Shared.Implementations;
using KwikNestaIdentity.Domain.Entities;
using KwikNestaIdentity.Infrastructure.Contracts;
using KwikNestaIdentity.Infrastructure.Data;

namespace KwikNestaIdentity.Infrastructure.Repositories
{
    internal class RefreshTokenRepository : Repository<RefreshToken>, IRefreshTokenRepository
    {
        public RefreshTokenRepository(IdentityServiceDbContext context) : base(context)
        {
        }
    }
}