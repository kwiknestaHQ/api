using KwikNesta.Shared.Models.Enumerations;
using Microsoft.AspNetCore.Http;

namespace KwikNesta.Shared.Extensions
{
    public static class FileValidation
    {
        private const long MaxImageBytes = 2 * 1024 * 1024;
        private const long MaxDocBytes = 2 * 1024 * 1024;
        private const long MaxVideoBytes = 10 * 1024 * 1024;
        private const long MaxAudioBytes = 2 * 1024 * 1014;
        public static readonly List<string> _validImage = [".jpg", ".jpeg", ".png"];
        public static readonly List<string> _validDocs = [".pdf", ".docx", ".xlsx", ".pptx", ".csv", ".doc", ".xls", ".ppt"];
        public static readonly List<string> _validAudio = [".mp3"];
        public static readonly List<string> _validVideo = [".mp4"];

        public static bool IsValid(string contentType, string extension)
        {
            extension = extension.ToLower();

            return contentType switch
            {
                "image/jpeg" => extension is ".jpg" or ".jpeg",
                "image/png" => extension == ".png",

                "application/pdf" => extension == ".pdf",

                "application/vnd.openxmlformats-officedocument.wordprocessingml.document"
                    => extension == ".docx",

                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                    => extension == ".xlsx",

                "application/vnd.openxmlformats-officedocument.presentationml.presentation"
                    => extension == ".pptx",

                "text/csv" => extension == ".csv",

                "video/mp4" => extension == ".mp4",

                "audio/mpeg" => extension == ".mp3",

                // Legacy Office
                "application/ms-office-legacy"
                    => extension is ".doc" or ".xls" or ".ppt",

                _ => false
            };
        }

        public static bool IsValidFileType(string extension, EUploadType type)
        {
            return type switch
            {
                EUploadType.Image => _validImage.Contains(extension),
                EUploadType.Docs => _validDocs.Contains(extension),
                EUploadType.Video => _validVideo.Contains(extension),
                EUploadType.Audio => _validAudio.Contains(extension),
                _ => false,
            };
        }

        public static bool IsValidFile(this IFormFile file, EUploadType type)
        {
            if (file == null) return false;
            var length = file.Length;
            return type switch
            {
                EUploadType.Image => length <= MaxImageBytes,
                EUploadType.Docs => length <= MaxDocBytes,
                EUploadType.Video => length <= MaxVideoBytes,
                EUploadType.Audio => length <= MaxAudioBytes,
                _ => false,
            };
        }
    }
}