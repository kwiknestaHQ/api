using KwikNesta.Mediator.Cores.Abstractions;
using KwikNesta.Shared.Constants;
using KwikNesta.Shared.Extensions;
using KwikNesta.Shared.Implementations;
using KwikNesta.Shared.Models.Enumerations.Infra;
using KwikNesta.Shared.Models.Settings;
using KwikNesta.Shared.Responses;
using KwikNesta.Shared.ServiceCommands.Payment;
using KwikNesta.Shared.ServiceDTOs.Payment;
using KwikNestaPayment.Application.Validations;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace KwikNestaPayment.Application.Handlers
{
    public class InitiatePaystackRefundCommandHandler(IOptions<KNApplicationSettings> options,
                                                    HttpClient http,
                                                    ILogger<InitiatePaystackRefundCommandHandler> logger) 
        : IKNRequestHandler<InitiatePaystackRefundCommand, Response<PaystackRefundInitiationResponseData>>
    {
        private readonly HttpClient _http = http;
        private readonly ILogger<InitiatePaystackRefundCommandHandler> _logger = logger;
        private readonly PaystackSettings _paystackSettings = options.Value.Paystack ?? 
            throw new ArgumentNullException(nameof(PaystackSettings));

        public async Task<Response<PaystackRefundInitiationResponseData>> HandleAsync(InitiatePaystackRefundCommand request, CancellationToken cancellationToken)
        {
            var validator = new InitiatePaystackRefundCommandValidator().Validate(request);
            if (!validator.IsValid)
            {
                return Response<PaystackRefundInitiationResponseData>.Fail(validator.Errors.FirstOrDefault()?.ErrorMessage ?? 
                    PaymentResponses.InvalidRequest,
                    StatusCodes.Status400BadRequest);
            }

            var httpRequest = new HttpRequestMessage(HttpMethod.Post, $"{_paystackSettings.BaseUrl}/refund");
            httpRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _paystackSettings.PrivateKey);

            httpRequest.Content = JsonContent.Create(new PaystackRefundRequest
            {
                Transaction = request.PaymentReference,
                Amount = request.Amount.ToMinorUnits(),
            }, options: new JsonSerializerOptions
            {
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            var response = await _http.SendAsync(httpRequest, cancellationToken);
            var content = await response.Content.ReadAsStringAsync(cancellationToken);
            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError("=== Payment Refund failed with response: {content} ===", content);
                return Response<PaystackRefundInitiationResponseData>.Fail(string.Format(PaymentResponses.RefundFailed, 
                    request.PaymentReference), 
                    (int)response.StatusCode);
            }

            var data = JsonSerializer.Deserialize<PaystackRefundInitiationResponse>(content);
            if(data == null || !data.Status)
            {
                return Response<PaystackRefundInitiationResponseData>.Fail(data?.Message ?? PaymentResponses.InvalidRequest,
                    StatusCodes.Status400BadRequest);
            }

            AppAudit.Write(request.UserId,
                request.UserEmail,
                EAuditAction.InitializedRefund,
                EAuditDomain.Refund,
                request.PaymentReference,
                string.Empty,
                AppConstants.Initiator);

            return Response<PaystackRefundInitiationResponseData>.Ok(data.Data);
        }
    }
}