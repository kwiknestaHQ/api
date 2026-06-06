using Hangfire.Console;
using Hangfire.Server;
using KwikNesta.Mediator.Cores.Abstractions;
using KwikNesta.Shared.Contracts;
using KwikNesta.Shared.Helpers;
using KwikNesta.Shared.Models.Enumerations.Property;
using KwikNesta.Shared.Models.Settings;
using KwikNesta.Shared.ServiceCommands.Property;
using KwikNestaProperty.Infrastructure.Services.Abstraction;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace KwikNestaProperty.Infrastructure.Services
{
    public class PropertyBackgroundService(IPropertyRepositoryManager repository,
                                    IReverseGeocodeService geocodeService, 
                                    IKNMediator mediator, 
                                    IOptions<KNApplicationSettings> options)
        : IPropertyBackgroundService
    {
        private readonly IPropertyRepositoryManager _repository = repository;
        private readonly IReverseGeocodeService _geocodeService = geocodeService;
        private readonly IKNMediator _mediator = mediator;
        private const double MaxDistanceTolerance = 0.2;
        private readonly AgoraSetting _agoraSettings = options.Value.Agora ??
            throw new ArgumentNullException(nameof(AgoraSetting));

        public async Task VerifyLocation(Guid propertyId, PerformContext context)
        {
            context.WriteLine("Started property location verification for: {0}", propertyId);
            var location = await _repository.PropertyLocation
                .FirstOrDefault(pl => pl.PropertyId == propertyId, true);
            if (location == null)
            {
                context.WriteLine("No location record found for property: {0}", propertyId);
                return;
            }

            location.VerificationStatus = ELocationVerificationStatus.Pending;

            var reverseGeocodeResult = await _geocodeService
                       .ReverseGeocoder(location.Latitude.ToString(), location.Longitude.ToString());

            if (!reverseGeocodeResult.IsSuccessStatusCode)
            {
                var error = JsonSerializer.Serialize(reverseGeocodeResult.Error);
                context.WriteLine("Address could not be verified at the moment: {0}", error);
                return;
            }

            if (reverseGeocodeResult.Content == null || reverseGeocodeResult.Content.Address == null)
            {
                context.WriteLine("Address verification response returned null for property: {0}", propertyId);
                return;
            }

            var result = reverseGeocodeResult.Content;
            var address = result.Address;
            if (address.Country != location.Country)
            {
                location.VerificationStatus = ELocationVerificationStatus.Failed;
                await _repository.SaveAsync();
                context.WriteLine("Invalid country");
                return;
            }

            if (!result.DisplayName.Contains(location.City) || !result.DisplayName.Contains(location.State))
            {
                location.VerificationStatus = ELocationVerificationStatus.Failed;
                await _repository.SaveAsync();
                context.WriteLine("Coordinates do not match address");
                return;
            }

            if (double.TryParse(result.Latitude, out var @lat) && double.TryParse(result.Longitude, out var @lon))
            {
                var distance = LocationHelpers.GetDistance(location.Latitude,
                                    location.Longitude,
                                    @lat,
                                    @lon);

                if (distance > MaxDistanceTolerance)
                {
                    location.VerificationStatus = ELocationVerificationStatus.Failed;
                    context.WriteLine("Address does not match coordinates");
                }
                else
                {
                    location.PostalCode = result.Address.PostCode;
                    location.VerificationStatus = ELocationVerificationStatus.Verified;
                    location.LastUpdatedOn = DateTime.UtcNow;
                    context.WriteLine("Property Location successfully verified.");
                }
            }
            else
            {
                location.VerificationStatus = ELocationVerificationStatus.Failed;
                context.WriteLine("Invalid coordinates returned");
            }

            await _repository.SaveAsync();
        }

        public async Task RunSettlementsInitiationAsync(PerformContext context, 
            CancellationToken cancellationToken)
        {
            var startTime = DateTime.UtcNow;
            context.WriteLine($"===[RunSettlementsInitiationAsync] Starting processing at {startTime}===");

            var sessionsDueForSettlement = await _repository.ViewingSession
                .Get(s => !s.IsDeprecated &&
                        s.SetlementStatus != ESSessionettlementStatus.Scheduled &&
                        s.SetlementStatus != ESSessionettlementStatus.Completed &&
                        s.IsExpired(_agoraSettings.ExpiryInSeconds))
                .Take(100)
                .ToListAsync(cancellationToken);

            foreach (var session in sessionsDueForSettlement.WithProgress(context))
            {
                context.WriteLine($"===[RunSettlementsInitiationAsync] Processing {session.Id}===");

                var settlementInitiationResult = await _mediator.SendAsync(new TryInitiateSettlementCommand
                {
                    Channel = session.ChannelName,
                }, cancellationToken);

                if (!settlementInitiationResult.Success)
                {
                    context.WriteLine($"===[RunSettlementsInitiationAsync] Settlement Initialization failed for session {session.Id}===");
                }
                else
                {
                    context.WriteLine($"===[RunSettlementsInitiationAsync] Settlement Initialization succeeded for session {session.Id}===");
                }
            }

            var endTime = DateTime.UtcNow;
            var duration = (endTime - startTime).TotalSeconds;
            context.WriteLine($"===[RunSettlementsInitiationAsync] Completed processing at {endTime}. Process ran for {duration} seconds.===");
        }
    }
}