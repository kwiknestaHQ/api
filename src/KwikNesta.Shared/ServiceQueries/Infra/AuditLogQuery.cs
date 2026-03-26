using KwikNesta.Mediator.Cores.Abstractions;
using KwikNesta.Shared.Models.Enumerations.Infra;
using KwikNesta.Shared.Responses;
using KwikNesta.Shared.ServiceDTOs.Infra;

namespace KwikNesta.Shared.ServiceQueries.Infra
{
    public class AuditLogQuery : BasePageQuery, IKNRequest<PagedResponse<AuditLogResponseDto>>
    {
        public string DomainId { get; set; } = default!;
        public EAuditAction? Action { get; set; }
    }

    public class AuditLogClientQuery : BasePageQuery
    {
        public EAuditAction? Action { get; set; }
    }

    public class AdminAuditLogClientQuery : BasePageQuery, IKNRequest<PagedResponse<AuditLogResponseDto>>
    {
        public string? Search { get; set; }
        public EAuditAction? Action { get; set; }
    }
}