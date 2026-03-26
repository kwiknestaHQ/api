using Hangfire;
using KwikNesta.Shared.Contracts;
using KwikNesta.Shared.Models.Enumerations.Infra;

namespace KwikNesta.Shared.Implementations
{
    public class Notifications
    {
        public static void SendEmail(string to, 
                                    string subject, 
                                    string message)
        {
            BackgroundJob.Enqueue<INotificationService>(x => 
                                x.SendEmailAsync(to, subject, message, message));
        }

        public static void SendEmail(string to, 
                                    string subject, 
                                    string message, 
                                    byte[] fileBytes, 
                                    string fileName, 
                                    EContentTypes contentType)
        {
            BackgroundJob.Enqueue<INotificationService>(x => 
                                x.SendEmailAsync(to, subject, message, message, fileBytes, fileName, contentType));
        }

        public static void SendScheduledEmail(string to,
                                    string subject,
                                    string message,
                                    int scheduleHours)
        {
            BackgroundJob.Schedule<INotificationService>(x => 
                                x.SendEmailAsync(to, subject, message, message),
                                DateTimeOffset.UtcNow.AddHours(scheduleHours));
        }

        public static void SendScheduledEmail(string to,
                                    string subject,
                                    string message,
                                    byte[] fileBytes,
                                    string fileName,
                                    EContentTypes contentType,
                                    int scheduleHours)
        {
            BackgroundJob.Schedule<INotificationService>(x => 
                                x.SendEmailAsync(to, subject, message, message, fileBytes, fileName, contentType),
                                DateTimeOffset.UtcNow.AddHours(scheduleHours));
        }
    }
}
