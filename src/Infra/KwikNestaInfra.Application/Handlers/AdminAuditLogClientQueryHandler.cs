using KwikNesta.Mediator.Cores.Abstractions;
using KwikNesta.Shared.Extensions;
using KwikNesta.Shared.Models.Enumerations.Infra;
using KwikNesta.Shared.Responses;
using KwikNesta.Shared.ServiceDTOs.Infra;
using KwikNesta.Shared.ServiceQueries.Infra;
using KwikNestaInfra.Infrastructure;

namespace KwikNestaInfra.Application.Handlers
{
    public class AdminAuditLogClientQueryHandler(IInfraRepositoryManager infraRepository)
        : IKNRequestHandler<AdminAuditLogClientQuery, PagedResponse<AuditLogResponseDto>>
    {
        private readonly IInfraRepositoryManager _repository = infraRepository;

        public async Task<PagedResponse<AuditLogResponseDto>> HandleAsync(AdminAuditLogClientQuery request, CancellationToken cancellationToken)
        {
            var audits = _repository.AuditLog
               .Get(a => !a.IsDeprecated && a.Domain == EAuditDomain.SystemAdmin);

            if (!string.IsNullOrWhiteSpace(request.Search))
            {
                var search = request.Search.ToLower();
                audits = audits.Where(a => a.UserName.ToLower().Contains(search));
            }

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