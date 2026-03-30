using KwikNesta.Shared.Models;
using Refit;

namespace KwikNesta.Shared.Contracts
{
    public interface IReverseGeocodeService
    {
        [Get("/reverse")]
        Task<ApiResponse<ReverseGeocodeResult>> ReverseGeocoder([Query] string lat,
                                                  [Query] string lon,
                                                  [Query] string format = "json");
    }
}