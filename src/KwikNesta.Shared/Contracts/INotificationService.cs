using KwikNesta.Shared.Models.Enumerations.Infra;

namespace KwikNesta.Shared.Contracts
{
    public interface INotificationService
    {
        Task SendEmailAsync(string to, string subject, string textPart, string html);
        Task SendEmailAsync(string to, string subject, string textPart, string html, byte[] attachment, string fileName, EContentTypes contentType);
    }
}