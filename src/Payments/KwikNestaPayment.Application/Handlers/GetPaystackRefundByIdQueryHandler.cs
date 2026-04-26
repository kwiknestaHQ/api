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
    public class GetPaystackRefundByIdQueryHandler(IOptions<KNApplicationSettings> options, 
                                                HttpClient http, 
                                                ILogger<GetPaystackRefundByIdQueryHandler> logger) 
        : IKNRequestHandler<GetPaystackRefundByIdQuery, Response<PaystackRefundQueryResponse>>
    {
        private readonly PaystackSettings _paystackSettings = options.Value.Paystack ?? 
            throw new ArgumentNullException(nameof(PaystackSettings));
        private readonly HttpClient _http = http;
        private readonly ILogger<GetPaystackRefundByIdQueryHandler> _logger = logger;

        public async Task<Response<PaystackRefundQueryResponse>> HandleAsync(GetPaystackRefundByIdQuery request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.RefundId))
            {
                return Response<PaystackRefundQueryResponse>.Fail(PaymentResponses.InvalidRequest, 
                    StatusCodes.Status400BadRequest);
            }

            var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"{_paystackSettings.BaseUrl}/refund/{request.RefundId}");
            httpRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _paystackSettings.PrivateKey);

            var response = await _http.SendAsync(httpRequest, cancellationToken);
            var content = await response.Content.ReadAsStringAsync(cancellationToken);
            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError("=== Payment Refund Query failed with response: {content} ===", content);
                return Response<PaystackRefundQueryResponse>.Fail(string.Format(PaymentResponses.RefundQueryFailed,
                    request.RefundId),
                    (int)response.StatusCode);
            }

            var data = JsonSerializer.Deserialize<PaystackRefundQueryResponse>(content)!;
            return Response<PaystackRefundQueryResponse>.Ok(data);
        }
    }
}
