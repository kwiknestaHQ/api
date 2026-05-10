using KwikNesta.Shared.Extensions;
using KwikNesta.Shared.Models.CsApis;
using KwikNesta.Shared.Models.Enumerations.Infra;
using KwikNesta.Shared.ServiceDTOs.Infra;
using KwikNestaInfra.Domain.Entities;

namespace KwikNestaInfra.Infrastructure
{
    public class InfraObjectFactory
    {
        public static AgoraWebhookEventLog Initialize(string id,
                                            string channel,
                                            EAgoraEvent type,
                                            long ts,
                                            EAgoraModule module,
                                            EAgoraPlatform platform = EAgoraPlatform.Other,
                                            uint? uid = null,
                                            int? duration = null,
                                            int? reason = null)
        {
            return new AgoraWebhookEventLog
            {
                Id = id,
                Channel = channel,
                Type = type,
                Module = module,
                UId = uid,
                Timestamp = ts.ToUtcDateTime(),
                Platform = platform,
                Duration = duration,
                Reason = reason
            };
        }

        public static KNCountry Map(CsCountry client)
        {
            return new KNCountry
            {
                Capital = client.Capital,
                Currency = client.Currency,
                CurrencyName = client.Currency_Name,
                CurrencySymbol = client.Currency_Symbol,
                Emoji = client.Emoji,
                EmojiUnicode = client.EmojiU,
                ISO2 = client.ISO2,
                ISO3 = client.ISO3,
                Latitude = client.Latitude,
                Longitude = client.Longitude,
                Name = client.Name,
                Nationality = client.Nationality,
                Native = client.Native,
                NumericCode = client.Numeric_Code,
                PhoneCode = client.PhoneCode,
                Region = client.Region,
                RegionId = client.Region_Id,
                SubRegion = client.SubRegion,
                SubRegionId = client.Subregion_Id,
                TLD = client.TLD,
            };
        }

        public static KNState Map(Guid countryId, CsState client)
        {
            return new KNState
            {
                CountryCode = client.Country_Code,
                ISO2 = client.ISO2,
                Latitude = client.Latitude,
                Longitude = client.Longitude,
                Name = client.Name,
                Type = client.Type,
                CountryId = countryId,
            };
        }

        public static List<KNCity> Map(Guid stateId,
                                    Guid countryId,
                                    List<CsCity> cities)
        {
            var result = new List<KNCity>();
            foreach (var city in cities)
            {
                result.Add(new KNCity
                {
                    Latitude = city.Latitude,
                    Longitude = city.Longitude,
                    Name = city.Name,
                    StateId = stateId,
                    CountryId = countryId
                });
            }
            return result;
        }

        public static CountryDto Map(KNCountry country)
        {
            return new CountryDto
            {
                Id = country.Id,
                Name = country.Name,
                ISO2 = country.ISO2,
                ISO3 = country.ISO3,
                PhoneCode = country.PhoneCode,
                Currency = country.Currency,
                CurrencyName = country.CurrencyName,
                CurrencySymbol = country.CurrencySymbol,
                TLD = country.TLD,
                Longitude = country.Longitude,
                Latitude = country.Latitude,
                Emoji = country.Emoji,
                EmojiUnicode = country.EmojiUnicode,
                IsActive = country.IsActive
            };
        }
    }
}