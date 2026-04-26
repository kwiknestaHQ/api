using KwikNesta.Mediator.Cores.Abstractions;
using KwikNesta.Shared.Extensions;
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
    public class InitiateTransferCommandHandler(IOptions<KNApplicationSettings> options,
                                                HttpClient http,
                                                ILogger<InitiateTransferCommandHandler> logger)
        : IKNRequestHandler<InitiateTransferCommand, Response<InitiateTransferResponse>>
    {
        private readonly PaystackSettings _paystackSettings = options.Value.Paystack ??
            throw new ArgumentNullException(nameof(PaystackSettings));
        private readonly HttpClient _http = http;
        private readonly ILogger<InitiateTransferCommandHandler> _logger = logger;

        public async Task<Response<InitiateTransferResponse>> HandleAsync(InitiateTransferCommand request, CancellationToken cancellationToken)
        {
            var validator = new InitiateTransferCommandValidator().Validate(request);
            if (!validator.IsValid)
            {
                return Response<InitiateTransferResponse>.Fail(validator.Errors.FirstOrDefault()?.ErrorMessage ??
                    PaymentResponses.InvalidRequest,
                    StatusCodes.Status400BadRequest);
            }

            var httpRequest = new HttpRequestMessage(HttpMethod.Post, $"{_paystackSettings.BaseUrl}/transfer");
            httpRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _paystackSettings.PrivateKey);

            httpRequest.Content = JsonContent.Create(new InitiateTransferRequest
            {
                Amount = request.Amount.ToMinorUnits(),
                Recipient = request.RecipientCode,
                Reason = request.Narration
            }, options: new JsonSerializerOptions
            {
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            var response = await _http.SendAsync(httpRequest, cancellationToken);
            var content = await response.Content.ReadAsStringAsync(cancellationToken);
            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError("=== Transfer Initialization failed with response: {content} ===", content);
                return Response<InitiateTransferResponse>.Fail(PaymentResponses.PaymentInitFailed, (int)response.StatusCode);
            }

            var data = JsonSerializer.Deserialize<InitiateTransferResponse>(content)!;
            return Response<InitiateTransferResponse>.Ok(data);
        }
    }
}