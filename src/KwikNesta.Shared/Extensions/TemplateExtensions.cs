using Amazon.Runtime.Internal.Transform;
using Amazon.S3.Model;
using KwikNesta.Shared.Constants;
using KwikNesta.Shared.Models.Enumerations.Property;
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

        public static string GetViewingSessionNotification(this IHostEnvironment host,
                                                                string firstName,
                                                                string propertyTitle,
                                                                string supportEmail,
                                                                DateTime scheduleDate,
                                                                string link,
                                                                bool isVirtual,
                                                                string address = "",
                                                                int reminderMinutes = 30)
        {
            var templateName = isVirtual ? "virtual-viewing.html" : "physical-viewing.html";
            var path = Path.Combine(host.ContentRootPath, "wwwroot", "templates", templateName);
            if (!File.Exists(path))
            {
                throw new Exception($"Path, {path}, not found");
            }

            var virtualTemplate = File.ReadAllText(path);
            if (string.IsNullOrEmpty(virtualTemplate))
            {
                throw new ArgumentNullException("Email template content can not be empty");
            }

            var content = RenderTemplate(virtualTemplate, new Dictionary<string, string>
            {
                { "{{FIRST_NAME}}", firstName },
                { "{{PROPERTY_NAME}}", propertyTitle },
                { "{{DATE}}", scheduleDate.FormatAsWat("ddd, MMM dd yyyy", false) },
                { "{{TIME}}", scheduleDate.FormatAsWat("h:mm tt") },
                { "{{LINK}}", link },
                { "{{REMINDER_MINUTES}}", reminderMinutes.ToString() },
                { "{{ADDRESS}}", address }
            });

            var layoutPath = Path.Combine(host.ContentRootPath, "wwwroot", "templates", "email-layout.html");
            if (!File.Exists(layoutPath))
            {
                throw new Exception($"Path, {path}, not found");
            }

            var layoutPathTemplate = File.ReadAllText(layoutPath);
            if (string.IsNullOrEmpty(layoutPathTemplate))
            {
                throw new ArgumentNullException("Email template content can not be empty");
            }

            var finalHtml = layoutPathTemplate
                .Replace("{{CONTENT}}", content)
                .Replace("{{APP_NAME}}", AppConstants.Platform)
                .Replace("{{SUPPORT_EMAIL}}", supportEmail)
                .Replace("{{YEAR}}", DateTime.UtcNow.ToString("yyyy"));

            return finalHtml;
        }

        public static string GetViewingSessionReminderNotification(this IHostEnvironment host,
                                                                string firstName,
                                                                string supportEmail,
                                                                string link,
                                                                bool isVirtual,
                                                                int reminderMinutes = 30)
        {
            var path = Path.Combine(host.ContentRootPath, "wwwroot", "templates", "view-reminder.html");
            if (!File.Exists(path))
            {
                throw new Exception($"Path, {path}, not found");
            }

            var reminderTemplate = File.ReadAllText(path);
            if (string.IsNullOrEmpty(reminderTemplate))
            {
                throw new ArgumentNullException("Email template content can not be empty");
            }

            var content = RenderTemplate(reminderTemplate, new Dictionary<string, string>
            {
                { "{{FIRST_NAME}}", firstName },
                { "{{LINK}}", link },
                { "{{REMINDER_MINUTES}}", reminderMinutes.ToString() },
                { "{{REMINDER_BUTTON_TEXT}}", isVirtual ? "Join Viewing" : "Check In" }
            });

            var layoutPath = Path.Combine(host.ContentRootPath, "wwwroot", "templates", "email-layout.html");
            if (!File.Exists(layoutPath))
            {
                throw new Exception($"Path, {path}, not found");
            }

            var layoutPathTemplate = File.ReadAllText(layoutPath);
            if (string.IsNullOrEmpty(layoutPathTemplate))
            {
                throw new ArgumentNullException("Email template content can not be empty");
            }

            var finalHtml = layoutPathTemplate
                .Replace("{{CONTENT}}", content)
                .Replace("{{APP_NAME}}", AppConstants.Platform)
                .Replace("{{SUPPORT_EMAIL}}", supportEmail)
                .Replace("{{YEAR}}", DateTime.UtcNow.ToString("yyyy"));

            return finalHtml;
        }

        public static string RenderTemplate(string template, Dictionary<string, string> data)
        {
            foreach (var key in data)
            {
                template = template.Replace(key.Key, key.Value);
            }

            return template;
        }
    }
}