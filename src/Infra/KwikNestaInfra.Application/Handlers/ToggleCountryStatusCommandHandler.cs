using KwikNesta.Mediator.Cores.Abstractions;
using KwikNesta.Shared.Implementations;
using KwikNesta.Shared.Models.Enumerations.Infra;
using KwikNesta.Shared.Responses;
using KwikNesta.Shared.ServiceCommands.Infra;
using KwikNestaInfra.Infrastructure;

namespace KwikNestaInfra.Application.Handlers
{
    public class ToggleCountryStatusCommandHandler(IInfraRepositoryManager repository)
        : IKNRequestHandler<ToggleCountryStatusCommand, Response<string>>
    {
        private readonly IInfraRepositoryManager _repository = repository;

        public async Task<Response<string>> HandleAsync(ToggleCountryStatusCommand request, CancellationToken cancellationToken)
        {
            var country = await _repository.Country
                .FirstOrDefault(c => c.Id == request.Id && !c.IsDeprecated, true);
            if (country == null)
            {
                return Response<string>.Fail(InfraResponses.RecordNotFound, 404);
            }

            var message = country.IsActive ? "deactivated" : "activated";
            country.IsActive = !country.IsActive;
            country.LastUpdatedOn = DateTime.UtcNow;
            await _repository.SaveAsync();

            AppAudit.Write(request.LoggedInUserId,
                request.LoggedInUserEmail,
                EAuditAction.LocationToggle,
                EAuditDomain.Location,
                country.Id.ToString(),
                request.LoggedInUserIpAddress,
                message);

            return Response<string>.Ok(string.Format(InfraResponses.CountryToggled, country.Name, message));
        }
    }
}