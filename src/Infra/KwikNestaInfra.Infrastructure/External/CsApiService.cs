using KwikNesta.Shared.Models.CsApis;
using KwikNestaInfra.Infrastructure.Contracts;
using Microsoft.Extensions.Logging;
using System.Net.Http.Json;

namespace KwikNestaInfra.Infrastructure.External
{
    public class CsApiService(IHttpClientFactory factory,
                            ILogger<CsApiService> logger) 
        : ICsApiService
    {
        private readonly HttpClient _client = factory.CreateClient("CsApi");
        private readonly ILogger<CsApiService> _logger = logger;

        public async Task<(List<CsCountry> Countries, bool Success)> GetCountriesAsync()
        {
            var response = await _client.GetAsync("");
            if (response.IsSuccessStatusCode)
            {
                var countries = await response.Content.ReadFromJsonAsync<List<CsCountry>>();
                return (countries ?? [], true);
            }
            else
            {
                string errorMessage = await response.Content.ReadAsStringAsync();
                var error = $"[GetCountriesAsync]: {response.StatusCode} - {errorMessage}";
                _logger.LogError(error);
                return ([], false);
            }
        }

        public async Task<(CsCountry? Country, bool Success)> GetCountryAsync(string countryIso2)
        {
            var response = await _client.GetAsync($"{countryIso2}");
            if (response.IsSuccessStatusCode)
            {
                var country = await response.Content.ReadFromJsonAsync<CsCountry>();
                return (country, true);
            }
            else
            {
                string errorMessage = await response.Content.ReadAsStringAsync();
                var error = $"[GetCountryAsync]: {response.StatusCode} - {errorMessage}";
                _logger.LogError(error);
                return (null, false);
            }
        }

        public async Task<(List<CsState> States, bool Success)> GetStatesAsync(string countryIso2)
        {
            var response = await _client.GetAsync($"{countryIso2}/states");
            if (response.IsSuccessStatusCode)
            {
                var states = await response.Content.ReadFromJsonAsync<List<CsState>>();
                return (states ?? [], true);
            }
            else
            {
                string errorMessage = await response.Content.ReadAsStringAsync();
                var error = $"[GetStatesAsync]: {response.StatusCode} - {errorMessage}";
                _logger.LogError(error);
                return ([], false);
            }
        }

        public async Task<(CsState? State, bool Success)> GetStateAsync(string countryIso2, string stateIso2)
        {
            var response = await _client.GetAsync($"{countryIso2}/states/{stateIso2}");
            if (response.IsSuccessStatusCode)
            {
                var state = await response.Content.ReadFromJsonAsync<CsState>();
                return (state, true);
            }
            else
            {
                string errorMessage = await response.Content.ReadAsStringAsync();
                var error = $"[GetStateAsync]: {response.StatusCode} - {errorMessage}";
                _logger.LogError(error);
                return (null, false);
            }
        }

        public async Task<(List<CsCity> Cities, bool Success)> GetCitiesAsync(string countryIso2, string stateIso2)
        {
            var response = await _client.GetAsync($"{countryIso2}/states/{stateIso2}/cities");
            if (response.IsSuccessStatusCode)
            {
                var cities = await response.Content.ReadFromJsonAsync<List<CsCity>>();
                return (cities ?? [], true);
            }
            else
            {
                string errorMessage = await response.Content.ReadAsStringAsync();
                var error = $"[GetCitiesAsync]: {response.StatusCode} - {errorMessage}";
                _logger.LogError(error);
                return ([], false);
            }
        }
    }
}