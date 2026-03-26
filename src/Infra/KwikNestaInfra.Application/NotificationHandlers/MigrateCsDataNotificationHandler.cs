using KwikNesta.Mediator.Cores.Abstractions;
using KwikNesta.Shared.Extensions;
using KwikNesta.Shared.Implementations;
using KwikNesta.Shared.Models.Enumerations.Infra;
using KwikNesta.Shared.Models.Settings;
using KwikNesta.Shared.Responses;
using KwikNesta.Shared.ServiceNotifications.Infra;
using KwikNestaInfra.Domain.Entities;
using KwikNestaInfra.Infrastructure;
using KwikNestaInfra.Infrastructure.Contracts;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace KwikNestaInfra.Application.NotificationHandlers
{
    public class MigrateCsDataNotificationHandler(IInfraRepositoryManager repository,
                                        ICsApiService csApiService,
                                        ILogger<MigrateCsDataNotificationHandler> logger,
                                        IHostEnvironment host,
                                        IOptions<KNApplicationSettings> options) 
        : IKNNotificationHandler<MigrateCsDataNotification>
    {
        private readonly IInfraRepositoryManager _repository = repository;
        private readonly ICsApiService _csApiService = csApiService;
        private readonly ILogger<MigrateCsDataNotificationHandler> _logger = logger;
        private readonly IHostEnvironment _host = host;
        private readonly string _supportEmail = options.Value.AppAdmin.SupportEmail;

        public async Task HandleAsync(MigrateCsDataNotification notification, CancellationToken cancellationToken)
        {
            var startTime = DateTime.UtcNow;
            _logger.LogInformation("===[MigrateCsDataNotificationHandler] Migration Started===");
            var (Countries, Success) = await _csApiService.GetCountriesAsync();
            if(!Success)
            {
                _logger.LogWarning("===[MigrateCsDataNotificationHandler] Fetching countries failed.===");
                return;
            }

            foreach(var country in Countries)
            {
                _logger.LogInformation($"===[MigrateCsDataNotificationHandler] Migrating {country.Name}===");
                var countryToAdd = await _repository.Country.FirstOrDefault(c => c.ISO2 == country.ISO2);
                if (countryToAdd == null)
                {
                    var countryRequest = await _csApiService.GetCountryAsync(country.ISO2);
                    if (!countryRequest.Success || countryRequest.Country == null)
                    {
                        _logger.LogWarning($"===[MigrateCsDataNotificationHandler] Fetching country, {country.Name}, failed.===");
                        continue;
                    }

                    countryToAdd = ObjectFactory.Map(countryRequest.Country);
                    await _repository.Country.AddAsync(countryToAdd);

                    var timeZones = ParseTimeZones(countryRequest.Country.TimeZones, countryToAdd.Id);
                    await _repository.TimeZone.AddRangeAsync(timeZones);
                    await _repository.SaveAsync();
                }

                var existingState = await _repository.State.FirstOrDefault(s => s.CountryCode == country.ISO2);
                if (existingState != null)
                {
                    var citiesExist = await _repository.City.ExistsAsync(c => c.CountryId == existingState.CountryId);
                    if (citiesExist)
                    {
                        _logger.LogWarning($"===[MigrateCsDataNotificationHandler] {country.Name} already exists in the DB.===");
                        continue;
                    }
                }

                _logger.LogInformation($"===[MigrateCsDataNotificationHandler] Fetching states for {country.Name}.===");
                var (States, StateSuccess) = await _csApiService.GetStatesAsync(country.ISO2);
                if (!StateSuccess)
                {
                    _logger.LogWarning($"===[MigrateCsDataNotificationHandler] Fetching states for {country.Name} failed.===");
                    continue;
                }
                
                var statesToAdd = new List<KNState>();
                var citiesToAdd = new List<KNCity>();
                foreach(var state in States)
                {
                    _logger.LogInformation($"===[MigrateCsDataNotificationHandler] Migrating {state.Name}, {country.Name}===");

                    var stateRequest = await _csApiService.GetStateAsync(country.ISO2, state.ISO2);
                    if (!stateRequest.Success || stateRequest.State == null)
                    {
                        _logger.LogWarning($"===[MigrateCsDataNotificationHandler] Fetching state, {state.Name}, {country.Name}, failed.===");
                        continue;
                    }

                    var stateToAdd = ObjectFactory.Map(countryToAdd.Id, stateRequest.State);
                    statesToAdd.Add(stateToAdd);
                    var (Cities, CitySuccess) = await _csApiService.GetCitiesAsync(country.ISO2, state.ISO2);
                    if (!CitySuccess)
                    {
                        _logger.LogWarning($"===[MigrateCsDataNotificationHandler] Fetching cities for {state.Name}, {country.Name} failed.===");
                        continue;
                    }

                    citiesToAdd.AddRange(ObjectFactory.Map(stateToAdd.Id, countryToAdd.Id, Cities));
                    _logger.LogInformation($"===[MigrateCsDataNotificationHandler] Migrated {state.Name}, {country.Name}===");
                }

                if (statesToAdd.Count > 0)
                {
                    await _repository.State.AddRangeAsync(statesToAdd);
                    if (citiesToAdd.Count > 0)
                    {
                        await _repository.City.AddRangeAsync(citiesToAdd);
                    }

                    await _repository.SaveAsync();
                    _logger.LogInformation($"===[MigrateCsDataNotificationHandler] Migrated data for {country.Name}===");
                }
            }

            AppAudit.Write(notification.LoggedInUserId, 
                notification.LoggedInUserEmail, 
                EAuditAction.MigratedLocationData, 
                EAuditDomain.SystemAdmin,
                Guid.NewGuid().ToString(),
                notification.LoggedInUserIpAddress);

            Notifications.SendEmail(notification.LoggedInUserEmail,
                                InfraResponses.LocationDataloadCompletedSubject,
                                _host.GetInformationalNotification("Admin",
                                    string.Format(InfraResponses.LocationDataloadCompletedMessage,
                                        _host.EnvironmentName,
                                        notification.LoggedInUserIpAddress,
                                        startTime.FormatDate(),
                                        startTime.FormatDureation(DateTime.UtcNow)),
                                    _supportEmail));

            _logger.LogInformation("===[MigrateCsDataNotificationHandler] Migration Completed===");
        }

        private List<KNTimeZone> ParseTimeZones(string timeZoneString, Guid countryId)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(timeZoneString))
                {
                    return [];
                }

                timeZoneString = timeZoneString.Trim().Replace("\\", "");
                var timeZones = JsonConvert.DeserializeObject<List<KNTimeZone>>(timeZoneString);
                foreach(var timeZone in timeZones)
                {
                    timeZone.CountryId = countryId;
                }
                return timeZones ?? [];
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return [];
            }
        }
    }
}