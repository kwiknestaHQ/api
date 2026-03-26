using KwikNesta.Shared.Implementations;
using KwikNestaProperty.Domain.Entities;
using KwikNestaProperty.Infrastructure.Contracts;
using KwikNestaProperty.Infrastructure.Data;

namespace KwikNestaProperty.Infrastructure.Repositories
{
    public class OwnershipVerificationRepository(PropertyServiceDbContext context) 
        : Repository<OwnershipVerificationRequest>(context), IOwnershipVerificationRepository
    {
    }
}