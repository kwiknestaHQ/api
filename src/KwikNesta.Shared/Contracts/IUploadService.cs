
using KwikNesta.Shared.Models.Enumerations;

namespace KwikNesta.Shared.Contracts
{
    public interface IUploadService
    {
        Task DeleteByUrlAsync(string fileUrl);
        Task DeleteFileAsync(string key);
        Task<string> UploadFileAsync(byte[] fileBytes, string fileExtension, string contentType, EUploadType type);
    }
}