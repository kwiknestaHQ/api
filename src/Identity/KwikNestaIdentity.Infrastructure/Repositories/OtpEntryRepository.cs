using KwikNesta.Shared.Implementations;
using KwikNestaIdentity.Domain.Entities;
using KwikNestaIdentity.Infrastructure.Contracts;
using KwikNestaIdentity.Infrastructure.Data;

namespace KwikNestaIdentity.Infrastructure.Repositories
{
    public class OtpEntryRepository : Repository<OtpEntry>, IOtpEntryRepository
    {
        public OtpEntryRepository(IdentityServiceDbContext context) : base(context)
        {
        }
    }
}