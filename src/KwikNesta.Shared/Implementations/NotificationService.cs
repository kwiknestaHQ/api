using KwikNesta.Shared.Constants;
using KwikNesta.Shared.Contracts;
using KwikNesta.Shared.Extensions;
using KwikNesta.Shared.Models;
using KwikNesta.Shared.Models.Enumerations.Infra;
using KwikNesta.Shared.Models.Settings;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace KwikNesta.Shared.Implementations
{
    public class NotificationService(HttpClient httpClient,
                            ILogger<NotificationService> logger,
                            IOptions<KNApplicationSettings> options) 
        : INotificationService
    {
        private readonly HttpClient _httpClient = httpClient;
        private readonly ILogger<NotificationService> _logger = logger;
        private readonly ResendSettings _resendSettings = options.Value.Resend;
        private readonly JsonSerializerOptions camelCaseOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };

        /// <summary>
        /// Send email without attachment
        /// </summary>
        /// <param name="to"></param>
        /// <param name="subject"></param>
        /// <param name="textPart"></param>
        /// <param name="html"></param>
        /// <returns></returns>
        public async Task SendEmailAsync(string to,
                                        string subject,
                                        string textPart,
                                        string html)
        {
            _logger.LogInformation($"[SendEmailAsync] Sending Email to: {to}");
            var json = JsonSerializer.Serialize(new EmailRequest
            {
                From = $"{AppConstants.Platform} <{_resendSettings.Sender}>",
                To = to,
                Text = textPart,
                Subject = subject,
                Html = html
            }, camelCaseOptions);

            using var httpRequest = new HttpRequestMessage(HttpMethod.Post,
                $"{_resendSettings.BaseUrl}/email");

            httpRequest.Headers.Add("Authorization", _resendSettings.ApiKey);
            httpRequest.Content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.SendAsync(httpRequest);
            var rawResponse = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError($"[SendEmailAsync] HTTP failure {response.StatusCode}: {rawResponse}");
            }
            else
            {
                _logger.LogInformation($"[SendEmailAsync] Email Sent to: {to}");
            }
        }

        /// <summary>
        /// Send email with attachment
        /// </summary>
        /// <param name="to"></param>
        /// <param name="subject"></param>
        /// <param name="textPart"></param>
        /// <param name="html"></param>
        /// <param name="attachment"></param>
        /// <param name="fileName"></param>
        /// <param name="contentType"></param>
        /// <returns></returns>
        public async Task SendEmailAsync(string to, 
                                        string subject, 
                                        string textPart, 
                                        string html,
                                        byte[] attachment,
                                        string fileName,
                                        EContentTypes contentType)
        {
            _logger.LogInformation($"[SendEmailAsync] Sending Email With Attachment to: {to}");

            var base64 = Convert.ToBase64String(attachment);
            var json = JsonSerializer.Serialize(new EmailWithAttachmentRequest
            {
                From = $"{AppConstants.Platform} <{_resendSettings.Sender}>",
                To = to,
                Text = textPart,
                Subject = subject,
                Html = html,
                Attachments = new()
                {
                    new EmailAttachementsModel
                    {
                        Filename = fileName,
                        Content = base64,
                        Content_type = contentType.GetDescription()
                    }
                }
            }, camelCaseOptions);

            using var httpRequest = new HttpRequestMessage(HttpMethod.Post,
                $"{_resendSettings.BaseUrl}/email");

            httpRequest.Headers.Add("Authorization", _resendSettings.ApiKey);
            httpRequest.Content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.SendAsync(httpRequest);
            var rawResponse = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError($"[SendEmailAsync] HTTP failure {response.StatusCode}: {rawResponse}");
            }
            else
            {
                _logger.LogInformation($"[SendEmailAsync] Email With Attachment Sent to {to}");
            }
        }
    }
}