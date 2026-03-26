using KwikNesta.Shared.Models.Enumerations;
using Microsoft.AspNetCore.Http;

namespace KwikNesta.Shared.Extensions
{
    public static class FileExtensions
    {
        public static byte[] GetBytes(this IFormFile file)
        {
            ArgumentNullException.ThrowIfNull(file);

            using var ms = new MemoryStream();
            file.CopyTo(ms);
            return ms.ToArray();
        }

        public static EUploadType GetFileType(string extension)
        {
            if (FileValidation._validAudio.Contains(extension))
            {
                return EUploadType.Audio;
            }
            else if (FileValidation._validVideo.Contains(extension))
            {
                return EUploadType.Video;
            }
            else if (FileValidation._validImage.Contains(extension))
            {
                return EUploadType.Image;
            }
            else if (FileValidation._validDocs.Contains(extension))
            {
                return EUploadType.Docs;
            }
            else
            {
                throw new InvalidOperationException("Unsupported file type");
            }
        }

        public static List<string> GetValidExtensions(EUploadType type)
        {
            return type switch
            {
                EUploadType.Image => FileValidation._validImage,
                EUploadType.Docs => FileValidation._validDocs,
                EUploadType.Video => FileValidation._validVideo,
                EUploadType.Audio => FileValidation._validAudio,
                _ => [],
            };
        }
    }
}
