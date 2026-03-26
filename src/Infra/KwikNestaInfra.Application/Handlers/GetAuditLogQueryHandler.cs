using KwikNesta.Mediator.Cores.Abstractions;
using KwikNesta.Shared.Extensions;
using KwikNesta.Shared.Responses;
using KwikNesta.Shared.ServiceDTOs.Infra;
using KwikNesta.Shared.ServiceQueries.Infra;
using KwikNestaInfra.Infrastructure;

namespace KwikNestaInfra.Application.Handlers
{
    public class GetAuditLogQueryHandler : IKNRequestHandler<AuditLogQuery, PagedResponse<AuditLogResponseDto>>
    {
        private readonly IInfraRepositoryManager _infraRepository;

        public GetAuditLogQueryHandler(IInfraRepositoryManager infraRepository)
        {
            _infraRepository = infraRepository;
        }

        public async Task<PagedResponse<AuditLogResponseDto>> HandleAsync(AuditLogQuery request, CancellationToken cancellationToken)
        {
            var audits = _infraRepository.AuditLog
               .Get(a => !a.IsDeprecated && a.DomainId == request.DomainId);

            if (request.Action.HasValue)
            {
                audits = audits.Where(a => a.Action == request.Action.Value);
            }

            
            var data = audits
                .OrderByDescending(a => a.CreatedOn)
                .Select(a => new AuditLogResponseDto
                {
                    UserName = a.UserName,
                    Action = string.IsNullOrEmpty(a.Description) ? 
                        a.Action.GetDescription() : 
                        $"{a.Action.GetDescription()} ({a.Description})",
                    TimeStamp = a.CreatedOn,
                    IpAddress = a.IpAddress
                })
                .Paginate(request.Page, request.PageSize);

            await Task.CompletedTask;
            return data;
        }
    }
}