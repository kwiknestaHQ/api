using KwikNesta.Shared.Contracts;
using KwikNestaIdentity.Domain.Entities;

namespace KwikNestaIdentity.Infrastructure.Contracts
{
    public interface IOtpEntryRepository : IRepository<OtpEntry>
    {
    }
}