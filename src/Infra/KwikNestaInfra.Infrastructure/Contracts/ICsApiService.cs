using KwikNesta.Shared.Models.CsApis;

namespace KwikNestaInfra.Infrastructure.Contracts
{
    public interface ICsApiService
    {
        Task<(List<CsCity> Cities, bool Success)> GetCitiesAsync(string countryIso2, string stateIso2);
        Task<(List<CsCountry> Countries, bool Success)> GetCountriesAsync();
        Task<(CsCountry? Country, bool Success)> GetCountryAsync(string countryIso2);
        Task<(CsState? State, bool Success)> GetStateAsync(string countryIso2, string stateIso2);
        Task<(List<CsState> States, bool Success)> GetStatesAsync(string countryIso2);
    }
}
