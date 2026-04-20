using KwikNesta.Mediator.Cores.Abstractions;
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
    public class PaystackChargeCommandHandler(IOptions<KNApplicationSettings> options, 
                                            HttpClient http,
                                            ILogger<PaystackChargeCommandHandler> logger) 
        : IKNRequestHandler<PaystackChargeCommand, Response<PaystackInitResult>>
    {
        private readonly PaystackSettings _settings = options.Value.Paystack ?? 
            throw new ArgumentNullException(nameof(PaystackSettings));
        private readonly HttpClient _http = http;
        private readonly ILogger<PaystackChargeCommandHandler> _logger = logger;

        public async Task<Response<PaystackInitResult>> HandleAsync(PaystackChargeCommand request, CancellationToken cancellationToken)
        {
            var validator = new PaystackChargeCommandValidator().Validate(request);
            if (!validator.IsValid)
            {
                return Response<PaystackInitResult>.Fail(validator.Errors.FirstOrDefault()?.ErrorMessage ??
                    PaymentResponses.InvalidRequest, 
                    StatusCodes.Status400BadRequest);
            }

            var httpRequest = new HttpRequestMessage(HttpMethod.Post, 
                $"{_settings.BaseUrl}/transaction/initialize");

            httpRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _settings.PrivateKey);

            httpRequest.Content = JsonContent.Create(new PaystackInitRequest
            {
                Email = request.Email,
                Amount = request.Amount,
                Reference = request.Reference,
                Callback_url = _settings.CallbackUrl
            }, options: new JsonSerializerOptions
            {
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            var response = await _http.SendAsync(httpRequest, cancellationToken);
            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError("=== Payment Initialization failed with response: {Content} ===", response.Content);
                return Response<PaystackInitResult>.Fail(PaymentResponses.PaymentInitFailed, (int)response.StatusCode);
            }
            var content = await response.Content.ReadAsStringAsync(cancellationToken);
            var data = JsonSerializer.Deserialize<PaystackInitResult>(content)!;

            AppAudit.Write(request.Context.Id,
                request.Context.Email,
                EAuditAction.InitializedPayment,
                EAuditDomain.Payment,
                request.Reference,
                request.Context.IpAddress);

            return Response<PaystackInitResult>.Ok(data);
        }
    }
}