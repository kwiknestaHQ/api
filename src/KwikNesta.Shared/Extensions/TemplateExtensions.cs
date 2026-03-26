using KwikNesta.Shared.Constants;
using Microsoft.Extensions.Hosting;

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
    }
}