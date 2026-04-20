using KwikNesta.Shared.Implementations;
using KwikNestaProperty.Domain.Entities;
using KwikNestaProperty.Infrastructure.Contracts;
using KwikNestaProperty.Infrastructure.Data;

namespace KwikNestaProperty.Infrastructure.Repositories
{
    public class SessionParticipantRepository(PropertyServiceDbContext context) 
        : Repository<SessionParticipant>(context), ISessionParticipantRepository
    {
    }
}