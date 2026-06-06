using KwikNesta.Shared.Contracts;
using KwikNestaInfra.Domain.Entities;

namespace KwikNestaInfra.Infrastructure.Contracts
{
    public interface IAgoraTokenRepository : IRepository<AgoraToken>
    {
    }
}