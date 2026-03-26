using Amazon.S3;
using Amazon.S3.Model;
using KwikNesta.Shared.Contracts;
using KwikNesta.Shared.Extensions;
using KwikNesta.Shared.Models.Enumerations;
using KwikNesta.Shared.Models.Settings;
using Microsoft.Extensions.Options;
using System.Net;

namespace KwikNesta.Shared.Implementations
{
    public class UploadService : IUploadService
    {
        private readonly KNUploadSettings _settings;
        private readonly AmazonS3Client _amazonS3;
        public UploadService(IOptions<KNApplicationSettings> options)
        {
            _settings = options.Value.Upload ?? 
                throw new ArgumentNullException(nameof(KNUploadSettings));
            _amazonS3 = GetConfig(); 
        }

        /// <summary>
        /// Uploads assets to the server
        /// </summary>
        /// <param name="fileBytes"></param>
        /// <param name="fileExtension"></param>
        /// <param name="contentType"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public async Task<string> UploadFileAsync(byte[] fileBytes,
                                          string fileExtension,
                                          string contentType,
                                          EUploadType type)
        {
            var key = GetFileName(fileExtension, type);

            var presignedUrl = GeneratePresignedUploadUrl(key, 
                contentType, 
                TimeSpan.FromMinutes(10));

            await UploadWithPresignedUrlAsync(presignedUrl, fileBytes, contentType);
            return $"{_settings.CdnUrl}/{key}";
        }

        /// <summary>
        /// Delete by key
        /// Example:
        ///     images/202603/profile-uuid.jpg
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task DeleteFileAsync(string key)
        {
            var request = new DeleteObjectRequest
            {
                BucketName = _settings.BucketName,
                Key = key
            };

            await _amazonS3.DeleteObjectAsync(request);
        }

        /// <summary>
        /// Delete by file URL
        /// Example:
        ///  https://cdn.example.com/images/202603/profile-uuid.jpg
        /// </summary>
        /// <param name="fileUrl"></param>
        /// <returns></returns>
        public async Task DeleteByUrlAsync(string fileUrl)
        {
            var key = GetKeyFromCdnUrl(fileUrl);

            await DeleteFileAsync(key);
        }

        /// <summary>
        /// Extracts the storage key from the full CDN URL.
        /// Example:
        ///  https://cdn.example.com/images/202603/profile-uuid.jpg
        ///  => images/202603/profile-uuid.jpg
        /// </summary>
        string GetKeyFromCdnUrl(string fileUrl)
        {
            if (string.IsNullOrWhiteSpace(fileUrl))
                throw new ArgumentException("CDN URL cannot be empty.", nameof(fileUrl));

            if (string.IsNullOrWhiteSpace(_settings.CdnUrl))
                throw new ArgumentException("CDN base URL cannot be empty.", nameof(_settings.CdnUrl));

            // Remove base URL from full URL
            if (!fileUrl.StartsWith(_settings.CdnUrl))
                throw new ArgumentException("CDN URL does not match the configured CDN base URL.");

            var key = fileUrl[_settings.CdnUrl.Length..].TrimStart('/');

            if (string.IsNullOrWhiteSpace(key))
                throw new InvalidOperationException("Could not extract a valid key from the CDN URL.");

            return key;
        }

        string GetFileName(string fileExtension, EUploadType type)
        {
            return $"{type.GetDescription()}/{DateTime.UtcNow:yyyyMM}/{Guid.NewGuid()}{fileExtension}";
        }

        AmazonS3Client GetConfig()
        {
            var client = new AmazonS3Client(_settings.AccessKeyId,
                _settings.SecretAccessKey,
                new AmazonS3Config
                {
                    ServiceURL = _settings.Endpoint,
                    ForcePathStyle = true
                });
            return client;
        }

        string GeneratePresignedUploadUrl(string key, string contentType, TimeSpan expiry)
        {
            var request = new GetPreSignedUrlRequest
            {
                BucketName = _settings.BucketName,
                Key = key,
                Verb = HttpVerb.PUT,
                Expires = DateTime.UtcNow.Add(expiry),
                ContentType = contentType
            };

            return _amazonS3.GetPreSignedURL(request);
        }

        async Task UploadWithPresignedUrlAsync(string url, byte[] fileBytes, string contentType)
        {
            using var httpClient = new HttpClient();
            using var content = new ByteArrayContent(fileBytes);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(contentType);

            var response = await httpClient.PutAsync(url, content);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception($"Upload failed: {response.StatusCode} - {error}");
            }
        }
    }
}