using KwikNesta.Shared.Models.Enumerations.Property;

namespace KwikNesta.Shared.Extensions
{
    public static class StringExtensions
    {
        public static uint ToUId(this string str)
        {
            return (uint)Math.Abs(str.GetHashCode());
        }

        public static string CapitalizeEachWord(this string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return text;

            var words = text.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < words.Length; i++)
            {
                var word = words[i];

                words[i] = char.ToUpper(word[0]) +
                           (word.Length > 1 ? word.Substring(1).ToLower() : "");
            }

            return string.Join(" ", words);
        }

        public static string GetContentType(this string extension)
        {
            extension = extension.ToLower();

            return extension switch
            {
                ".jpg" or ".jpeg" => "image/jpeg",
                ".png" => "image/png",

                ".pdf" => "application/pdf",
                ".docx" => "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
                ".xlsx" => "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                ".pptx" => "application/vnd.openxmlformats-officedocument.presentationml.presentation",
                ".csv" => "text/csv",
                ".doc" or ".xls" or ".ppt" => "application/ms-office-legacy",

                ".mp4" => "video/mp4",

                ".mp3" => "audio/mpeg",

                _ => throw new InvalidOperationException("Unsupported file type")
            };
        }

        public static EMediaType GetMediaType(this string extension)
        {
            extension = extension.ToLower();

            return extension switch
            {
                ".jpg" or ".jpeg" or ".png" => EMediaType.Image,
                ".pdf" or ".docx" or ".xlsx" or ".pptx" or ".csv" or ".doc" or ".xls" or ".ppt" => EMediaType.Document,
                ".mp4" => EMediaType.Video,
                ".mp3" => EMediaType.Audio,

                _ => throw new InvalidOperationException("Unsupported file type")
            };
        }
    }
}