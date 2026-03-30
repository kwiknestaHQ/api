using KwikNesta.Shared.Models.Enumerations;
using KwikNesta.Shared.Models.Enumerations.Property;
using Microsoft.AspNetCore.Http;
using System.Security.Cryptography;

namespace KwikNesta.Shared.Extensions
{
    public static class FileExtensions
    {
        private static readonly string[] allowedDocsForVerification = ["image/jpeg", "image/png", "application/pdf"];
        public static bool AllowedFileTypesForVerification(this List<IFormFile> files)
        {
            foreach (var doc in files)
            {
                if (!allowedDocsForVerification.Contains(doc.ContentType))
                {
                    return false;
                }
            }

            return true;
        }

        public static async Task<bool> HasDuplicates(this List<IFormFile> files)
        {
            var seenHashes = new HashSet<string>();

            foreach (var file in files)
            {
                var hash = await ComputeHashAsync(file);

                if (!seenHashes.Add(hash))
                {
                    return true;
                }
            }

            return false;
        }

        public static byte[] GetBytes(this IFormFile file)
        {
            ArgumentNullException.ThrowIfNull(file);

            using var ms = new MemoryStream();
            file.CopyTo(ms);
            return ms.ToArray();
        }

        public static async Task<string> ComputeHashAsync(this IFormFile file)
        {
            using var stream = file.OpenReadStream();
            using var sha = SHA256.Create();

            var hashBytes = await sha.ComputeHashAsync(stream);
            return Convert.ToHexString(hashBytes);
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

        public static EMediaType GetMediaType(this EUploadType type)
        {
            return type switch
            {
                EUploadType.Image => EMediaType.Image,
                EUploadType.Docs => EMediaType.Document,
                EUploadType.Video => EMediaType.Video,
                EUploadType.Audio => EMediaType.Audio,
                _ => throw new InvalidOperationException("Unsupported upload type"),
            };
        }
    }
}
