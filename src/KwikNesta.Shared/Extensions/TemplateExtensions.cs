using Amazon.S3.Model;
using KwikNesta.Shared.Constants;
using KwikNesta.Shared.Models.Enumerations.Property;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Hosting;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace KwikNesta.Shared.Extensions
{
    public static class TemplateExtensions
    {
        public static string GetOtpNotification(this IHostEnvironment host,
                                                string name,
                                                string messageContent,
                                                string otp,
                                                string securityNotice,
                                                int expirationMinutes = 10)
        {
            var path = Path.Combine(host.ContentRootPath, "wwwroot", "templates", "otp-notification.html");
            if (!File.Exists(path))
            {
                throw new Exception($"Path, {path}, not found");
            }

            var template = File.ReadAllText(path);
            if (string.IsNullOrEmpty(template))
            {
                throw new ArgumentNullException("Email template content can not be empty");
            }

            var message = template.Replace("{{USER_NAME}}", name)
                .Replace("{{MESSAGE}}", messageContent)
                .Replace("{{OTP_CODE}}", otp)
                .Replace("{{SECURITY_NOTICE}}", securityNotice)
                .Replace("{{EXPIRATION_MINUTES}}", expirationMinutes.ToString())
                .Replace("{{APP_NAME}}", AppConstants.Platform)
                .Replace("{{YEAR}}", DateTime.UtcNow.ToString("yyyy"));

            return message;
        }

        public static string GetWelcomeNotification(this IHostEnvironment host,
                                                string name,
                                                string clientBaseUrl,
                                                string supportEmail)
        {
            var path = Path.Combine(host.ContentRootPath, "wwwroot", "templates", "welcome.html");
            if (!File.Exists(path))
            {
                throw new Exception($"Path, {path}, not found");
            }

            var template = File.ReadAllText(path);
            if (string.IsNullOrEmpty(template))
            {
                throw new ArgumentNullException("Email template content can not be empty");
            }

            var message = template.Replace("{{USER_NAME}}", name)
                .Replace("{{APP_NAME}}", AppConstants.Platform)
                .Replace("{{SUPPORT_EMAIL}}", supportEmail)
                .Replace("{{APP_URL}}", clientBaseUrl)
                .Replace("{{YEAR}}", DateTime.UtcNow.ToString("yyyy"));

            return message;
        }

        public static string GetInformationalNotification(this IHostEnvironment host,
                                                        string name,
                                                        string messageContent,
                                                        string? supportEmail = null)
        {
            var templateName = !string.IsNullOrWhiteSpace(supportEmail) ? "informational-support-notification.html" : "informational-notification.html";
            var path = Path.Combine(host.ContentRootPath, "wwwroot", "templates", templateName);
            if (!File.Exists(path))
            {
                throw new Exception($"Path, {path}, not found");
            }

            var template = File.ReadAllText(path);
            if (string.IsNullOrEmpty(template))
            {
                throw new ArgumentNullException("Email template content can not be empty");
            }

            var message = template.Replace("{{USER_NAME}}", name)
                .Replace("{{MESSAGE}}", messageContent)
                .Replace("{{SUPPORT_EMAIL}}", supportEmail)
                .Replace("{{APP_NAME}}", AppConstants.Platform)
                .Replace("{{YEAR}}", DateTime.UtcNow.ToString("yyyy"));

            return message;
        }

        public static string GetViewRequestResponseNotification(this IHostEnvironment host,
                                                        string name,
                                                        string propertyTitle,
                                                        string supportEmail,
                                                        string viewRequestLink,
                                                        string address,
                                                        DateTime inspectionDate,
                                                        EViewingType mode)
        {
            var path = Path.Combine(host.ContentRootPath, "wwwroot", "templates", "viewing-request-notification.html");
            if (!File.Exists(path))
            {
                throw new Exception($"Path, {path}, not found");
            }

            var template = File.ReadAllText(path);
            if (string.IsNullOrEmpty(template))
            {
                throw new ArgumentNullException("Email template content can not be empty");
            }

            var message = template.Replace("{{USER_NAME}}", name)
                .Replace("{{SUPPORT_EMAIL}}", supportEmail)
                .Replace("{{APP_NAME}}", AppConstants.Platform)
                .Replace("{{PROPERTY_TITLE}}", propertyTitle)
                .Replace("{{SCHEDULED_DATETIME}}", inspectionDate.FormatAsWat())
                .Replace("{{VIEWING_MODE}}", mode.GetDescription())
                .Replace("{{VIEW_REQUEST_LINK}}", viewRequestLink)
                .Replace("{{PROPERTY_LOCATION}}", address)
                .Replace("{{YEAR}}", DateTime.UtcNow.ToString("yyyy"));

            return message;
        }
    }
}