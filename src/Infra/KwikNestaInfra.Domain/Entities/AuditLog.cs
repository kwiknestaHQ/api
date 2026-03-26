using KwikNesta.Shared.Models;
using KwikNesta.Shared.Models.Enumerations.Infra;

namespace KwikNestaInfra.Domain.Entities
{
    public class AuditLog : BaseEntity
    {
        public string UserId { get; set; } = default!;
        public string UserName { get; set; } = default!;

        public EAuditAction Action { get; set; }
        public EAuditDomain Domain { get; set; }
        public string DomainId { get; set; } = default!;
        public string? IpAddress { get; set; }
        public string? Description { get; set; }
    }
}