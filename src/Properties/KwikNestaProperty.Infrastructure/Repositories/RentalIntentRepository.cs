using KwikNesta.Shared.Implementations;
using KwikNestaProperty.Domain.Entities;
using KwikNestaProperty.Infrastructure.Contracts;
using Microsoft.EntityFrameworkCore;

namespace KwikNestaProperty.Infrastructure.Repositories
{
    public class RentalIntentRepository(DbContext context) 
        : Repository<RentalIntent>(context), IRentalIntentRepository
    { }
}