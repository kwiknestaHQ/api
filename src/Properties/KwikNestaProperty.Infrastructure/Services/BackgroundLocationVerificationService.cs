using Hangfire.Console;
using Hangfire.Server;
using KwikNesta.Shared.Contracts;
using KwikNesta.Shared.Helpers;
using KwikNesta.Shared.Models.Enumerations.Property;
using System.Text.Json;

namespace KwikNestaProperty.Infrastructure.Services
{
    public class BackgroundLocationVerificationService(IPropertyRepositotyManager repository,
                                            IReverseGeocodeService geocodeService)
    {
        private readonly IPropertyRepositotyManager _repository = repository;
        private readonly IReverseGeocodeService _geocodeService = geocodeService;
        private const double MaxDistanceTolerance = 0.2;

        public async Task Verify(Guid propertyId, PerformContext context)
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
    }
}