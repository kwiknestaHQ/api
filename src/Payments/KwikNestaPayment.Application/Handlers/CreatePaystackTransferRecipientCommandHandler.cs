using KwikNesta.Mediator.Cores.Abstractions;
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
    public class CreatePaystackTransferRecipientCommandHandler(IOptions<KNApplicationSettings> options,
                                                HttpClient http,
                                                ILogger<CreatePaystackTransferRecipientCommandHandler> logger)
        : IKNRequestHandler<CreatePaystackTransferRecipientCommand, Response<CreatePaystackTransferResponseData>>
    {
        private readonly PaystackSettings _paystackSettings = options.Value.Paystack ??
            throw new ArgumentNullException(nameof(PaystackSettings));
        private readonly HttpClient _http = http;
        private readonly ILogger<CreatePaystackTransferRecipientCommandHandler> _logger = logger;

        public async Task<Response<CreatePaystackTransferResponseData>> HandleAsync(CreatePaystackTransferRecipientCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreatePaystackTransferRecipientCommandValidator().Validate(request);
            if (!validator.IsValid)
            {
                return Response<CreatePaystackTransferResponseData>.Fail(validator.Errors.FirstOrDefault()?.ErrorMessage ??
                    PaymentResponses.InvalidRequest,
                    StatusCodes.Status400BadRequest);
            }

            var httpRequest = new HttpRequestMessage(HttpMethod.Post, $"{_paystackSettings.BaseUrl}/transferrecipient");
            httpRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _paystackSettings.PrivateKey);

            httpRequest.Content = JsonContent.Create(new PaystackCreateTransferRequest
            {
                Account_number = request.AccountNumber,
                Bank_code = request.BankCode,
                Currency = request.Currency,
                Name = request.BankName,
                Type = request.AccountType
            }, options: new JsonSerializerOptions
            {
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            var response = await _http.SendAsync(httpRequest, cancellationToken);
            var content = await response.Content.ReadAsStringAsync(cancellationToken);
            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError("=== Transfer Creation failed with response: {content} ===", content);
                return Response<CreatePaystackTransferResponseData>.Fail(string.Format(PaymentResponses.TransferCreationFailed, 
                    request.AccountNumber), 
                    (int)response.StatusCode);
            }

            var data = JsonSerializer.Deserialize<CreatePaystackTransferResponse>(content)!;
            if (data == null || !data.Status)
            {
                return Response<CreatePaystackTransferResponseData>.Fail(data?.Message ?? 
                    PaymentResponses.InvalidRequest, 
                    StatusCodes.Status400BadRequest);
            }

            return Response<CreatePaystackTransferResponseData>.Ok(data.Data);
        }
    }
}