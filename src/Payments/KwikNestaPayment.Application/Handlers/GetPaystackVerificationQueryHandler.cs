using KwikNesta.Mediator.Cores.Abstractions;
using KwikNesta.Shared.Models.Settings;
using KwikNesta.Shared.Responses;
using KwikNesta.Shared.ServiceDTOs.Payment;
using KwikNesta.Shared.ServiceQueries.Payment;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Text.Json;

namespace KwikNestaPayment.Application.Handlers
{
    public class GetPaystackVerificationQueryHandler(HttpClient http, 
                                                IOptions<KNApplicationSettings> options) 
        : IKNRequestHandler<GetPaystackVerificationQuery, Response<PaystackVerifyResponse>>
    {
        private readonly HttpClient _http = http;
        private readonly PaystackSettings _paystackSettings = options.Value.Paystack;

        public async Task<Response<PaystackVerifyResponse>> HandleAsync(GetPaystackVerificationQuery request, CancellationToken cancellationToken)
        {
            var req = new HttpRequestMessage(HttpMethod.Get, 
                $"{_paystackSettings.BaseUrl}/transaction/verify/{request.Reference}");

            req.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _paystackSettings.PrivateKey);

            var response = await _http.SendAsync(req);

            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();

            var data = JsonSerializer.Deserialize<PaystackVerifyResponse>(content, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            })!;
            return Response<PaystackVerifyResponse>.Ok(data);
        }
    }
}