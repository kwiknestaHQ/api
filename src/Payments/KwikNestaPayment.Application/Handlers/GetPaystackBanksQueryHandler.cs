using KwikNesta.Mediator.Cores.Abstractions;
using KwikNesta.Shared.Models.Settings;
using KwikNesta.Shared.Responses;
using KwikNesta.Shared.ServiceDTOs.Payment;
using KwikNesta.Shared.ServiceQueries.Payment;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Text.Json;

namespace KwikNestaPayment.Application.Handlers
{
    public class GetPaystackBanksQueryHandler(IOptions<KNApplicationSettings> options,
                                                HttpClient http,
                                                ILogger<GetPaystackBanksQueryHandler> logger) 
        : IKNRequestHandler<GetPaystackBanksQuery, Response<List<PaystackBanksResponseData>>>
    {
        private readonly PaystackSettings _paystackSettings = options.Value.Paystack ??
            throw new ArgumentNullException(nameof(PaystackSettings));
        private readonly HttpClient _http = http;
        private readonly ILogger<GetPaystackBanksQueryHandler> _logger = logger;

        public async Task<Response<List<PaystackBanksResponseData>>> HandleAsync(GetPaystackBanksQuery request, CancellationToken cancellationToken)
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"{_paystackSettings.BaseUrl}/bank");
            httpRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _paystackSettings.PrivateKey);

            var response = await _http.SendAsync(httpRequest, cancellationToken);
            var content = await response.Content.ReadAsStringAsync(cancellationToken);
            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError("=== Paystack Banks Query failed with response: {content} ===", content);
                return Response<List<PaystackBanksResponseData>>.Fail(PaymentResponses.RefundQueryFailed,
                    (int)response.StatusCode);
            }

            var data = JsonSerializer.Deserialize<PaystackBanksResponse>(content)!;
            if(data == null || !data.Status)
            {
                return Response<List<PaystackBanksResponseData>>.Fail(data?.Message ?? 
                    PaymentResponses.InvalidRequest,
                    StatusCodes.Status400BadRequest);
            }

            var result = data.Data?
                .Where(b => b.IsActive && !b.IsDeleted)
                .ToList() ?? [];
            return Response<List<PaystackBanksResponseData>>.Ok(result);
        }
    }
}