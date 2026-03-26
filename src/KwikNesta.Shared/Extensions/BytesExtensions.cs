using System.IO.Compression;

namespace KwikNesta.Shared.Extensions
{
    public static class BytesExtensions
    {
        public static string DetectContentType(this byte[] bytes)
        {
            if (bytes.Length < 4)
                return "application/octet-stream";

            if (bytes[0] == 0x25 && bytes[1] == 0x50)
                return "application/pdf";

            if (bytes[0] == 0xFF && bytes[1] == 0xD8)
                return "image/jpeg";

            if (bytes[0] == 0x89 && bytes[1] == 0x50)
                return "image/png";

            if (bytes[0] == 0x49 && bytes[1] == 0x44 && bytes[2] == 0x33)
                return "audio/mpeg";

            if (bytes.Length > 12 &&
                bytes[4] == 0x66 && bytes[5] == 0x74 &&
                bytes[6] == 0x79 && bytes[7] == 0x70)
                return "video/mp4";

            // OLD Office (doc, xls, ppt)
            if (bytes[0] == 0xD0 && bytes[1] == 0xCF)
                return "application/ms-office-legacy";

            // ZIP (docx, xlsx, pptx)
            if (bytes[0] == 0x50 && bytes[1] == 0x4B)
                return DetectOfficeFromZip(bytes);

            return "application/octet-stream";
        }

        static string DetectOfficeFromZip(byte[] bytes)
        {
            try
            {
                using var stream = new MemoryStream(bytes);
                using var archive = new ZipArchive(stream);

                var entries = archive.Entries.Select(e => e.FullName);

                if (entries.Any(e => e.StartsWith("word/")))
                    return "application/vnd.openxmlformats-officedocument.wordprocessingml.document";

                if (entries.Any(e => e.StartsWith("xl/")))
                    return "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

                if (entries.Any(e => e.StartsWith("ppt/")))
                    return "application/vnd.openxmlformats-officedocument.presentationml.presentation";

                return "application/zip";
            }
            catch
            {
                return "application/zip";
            }
        }
    }
}