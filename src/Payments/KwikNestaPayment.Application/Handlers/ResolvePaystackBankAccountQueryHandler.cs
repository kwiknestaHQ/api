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
    public class ResolvePaystackBankAccountQueryHandler(IOptions<KNApplicationSettings> options,
                                                HttpClient http,
                                                ILogger<ResolvePaystackBankAccountQueryHandler> logger) 
        : IKNRequestHandler<ResolvePaystackBankAccountQuery, Response<PaystackAccountResolutionResponse>>
    {
        private readonly PaystackSettings _paystackSettings = options.Value.Paystack ??
            throw new ArgumentNullException(nameof(PaystackSettings));
        private readonly HttpClient _http = http;
        private readonly ILogger<ResolvePaystackBankAccountQueryHandler> _logger = logger;

        public async Task<Response<PaystackAccountResolutionResponse>> HandleAsync(ResolvePaystackBankAccountQuery request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.AccountNumber) || string.IsNullOrWhiteSpace(request.BankCode))
            {
                return Response<PaystackAccountResolutionResponse>.Fail(PaymentResponses.InvalidRequest,
                    StatusCodes.Status400BadRequest);
            }

            var httpRequest = new HttpRequestMessage(HttpMethod.Get, 
                $"{_paystackSettings.BaseUrl}/bank/resolve?account_number={request.AccountNumber}&bank_code={request.BankCode}");
            httpRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _paystackSettings.PrivateKey);

            var response = await _http.SendAsync(httpRequest, cancellationToken);
            var content = await response.Content.ReadAsStringAsync(cancellationToken);
            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError("=== Account Resolution Query failed with response: {content} ===", content);
                return Response<PaystackAccountResolutionResponse>.Fail(string.Format(PaymentResponses.RefundQueryFailed,
                    request.AccountNumber),
                    (int)response.StatusCode);
            }

            var data = JsonSerializer.Deserialize<PaystackAccountResolutionResponse>(content)!;
            return Response<PaystackAccountResolutionResponse>.Ok(data);
        }
    }
}
