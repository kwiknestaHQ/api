using Hangfire;
using Hangfire.Console;
using Hangfire.Server;
using KwikNesta.Mediator.Cores.Abstractions;
using KwikNesta.Shared.Contracts;
using KwikNesta.Shared.Extensions;
using KwikNesta.Shared.Helpers;
using KwikNesta.Shared.Implementations;
using KwikNesta.Shared.Models.Enumerations;
using KwikNesta.Shared.Models.Enumerations.Infra;
using KwikNesta.Shared.Models.Enumerations.Property;
using KwikNesta.Shared.Responses;
using KwikNesta.Shared.ServiceCommands.Property;
using KwikNesta.Shared.ServiceDTOs.Property;
using KwikNestaProperty.Domain.Entities;
using KwikNestaProperty.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace KwikNestaProperty.Application.Handlers
{
    public class UploadPropertyMediaCommandHandler(IPropertyRepositoryManager repository,
                                                IUploadService uploadService) 
        : IKNRequestHandler<UploadPropertyMediaCommand, Response<CreatePropertyResponseDto>>
    {
        private const int MaxUploadCount = 5;
        private readonly IPropertyRepositoryManager _repository = repository;
        private readonly IUploadService _uploadService = uploadService;

        public async Task<Response<CreatePropertyResponseDto>> HandleAsync(UploadPropertyMediaCommand request, CancellationToken cancellationToken)
        {
            if(!ValidationHelper.ValidUserContext(request.UserContext))
            {
                return Response<CreatePropertyResponseDto>.Fail(PropertyResponse.UserNotAuthenticated, 403);
            }

            var hasDuplicates = await request.Files.HasDuplicates();
            if (hasDuplicates)
            {
                return Response<CreatePropertyResponseDto>.Fail(PropertyResponse.DuplicatesDetectedInFile, 400);
            }

            var validation = await Validate(request.PropertyId, request.Files.Count);
            if (!validation.Success)
            {
                return validation;
            }

            var fileTuples = new List<(byte[] Bytes, string Ext, string cType, EUploadType uType)>();
            foreach(var file in request.Files)
            {
                var extension = Path.GetExtension(file.FileName).ToLower();
                var fileType = FileExtensions.GetFileType(extension);
                if (!file.IsValidFile(fileType))
                {
                    return Response<CreatePropertyResponseDto>.Fail(PropertyResponse.Invalidfile, 400);
                }

                var bytes = file.GetBytes();
                var detectedType = bytes.DetectContentType();
                if (!FileValidation.IsValid(detectedType, extension))
                {
                    return Response<CreatePropertyResponseDto>.Fail(PropertyResponse.UploadValidFiles, 400);
                }

                fileTuples.Add((bytes, extension, detectedType, fileType));
            }

            BackgroundJob.Enqueue(() 
                => UploadPropertyMediaAsync(request.PropertyId, 
                        fileTuples, 
                        request.UserContext.Id, 
                        request.UserContext.Email, 
                        request.UserContext.IpAddress, 
                        null!));

            AppAudit.Write(request.UserContext.Id,
                request.UserContext.Email,
                EAuditAction.UploadedPropertyImage,
                EAuditDomain.Property,
                validation.Data.Id.ToString(),
                request.UserContext.IpAddress,
                "Initiated Media Upload");

            return Response<CreatePropertyResponseDto>.Ok(validation.Data);
        }

        public async Task UploadPropertyMediaAsync(Guid propertyId,
                            List<(byte[] Bytes, string Ext, string cType, EUploadType uType)> payload,
                            string userId, 
                            string userEmail, 
                            string? userIpAddress,
                            PerformContext context)
        {
            var alreadyUploadedUrls = new List<string>();
            try
            {
                context.WriteLine("Media upload started for property: {0}", propertyId);
                var validation = await Validate(propertyId, payload.Count);
                if (!validation.Success)
                {
                    context.WriteLine(validation.Message);
                    return;
                }

                var uploadTasks = new List<Task<string>>();
                var mediaToAdd = new List<PropertyMedia>();
                var existsWithCover = await _repository.PropertyMedia
                        .ExistsAsync(pm => pm.PropertyId == propertyId && pm.IsPrimary);

                var order = 0;
                foreach (var (Bytes, Ext, cType, uType) in payload)
                {
                    var url = await _uploadService.UploadFileAsync(Bytes, Ext, cType, uType);
                    alreadyUploadedUrls.Add(url);
                    var hasCover = existsWithCover || mediaToAdd.Any(m => m.IsPrimary);
                    mediaToAdd.Add(new PropertyMedia
                    {
                        PropertyId = propertyId,
                        Url = url,
                        IsPrimary = !hasCover,
                        Type = uType.GetMediaType(),
                        Order = order
                    });

                    order++;
                }

                context.WriteLine("Adding {0} images for {1}", mediaToAdd.Count, validation.Data.Id);
                await _repository.PropertyMedia.AddRangeAsync(mediaToAdd);
                await _repository.SaveAsync();

                AppAudit.Write(userId,
                    userEmail,
                    EAuditAction.UploadedPropertyImage,
                    EAuditDomain.Property,
                    validation.Data.Id.ToString(),
                    userIpAddress,
                    "Completed Media Upload");

                context.WriteLine("Added {0} images for {1}", mediaToAdd.Count, validation.Data.Id);
            }
            catch (Exception)
            {
                alreadyUploadedUrls.ForEach(async url =>
                {
                    await _uploadService.DeleteByUrlAsync(url);
                });

                throw;
            }
        }

        private async Task<Response<CreatePropertyResponseDto>> Validate(Guid propertyId, int fileCount)
        {
            if (fileCount == 0)
            {
                return Response<CreatePropertyResponseDto>.Fail(PropertyResponse.UploadValidFiles, 400);
            }

            var property = await _repository.Property.FirstOrDefault(p => p.Id == propertyId);
            if (property == null)
            {
                return Response<CreatePropertyResponseDto>.Fail(
                    string.Format(PropertyResponse.RecordNotFound, "Property"),
                    404);
            }

            var propertyMedia = await _repository.PropertyMedia
                .Get(pm => pm.PropertyId == propertyId && pm.Type != EMediaType.Document)
                .ToListAsync();

            var remaining = MaxUploadCount - propertyMedia.Count;
            if (fileCount > remaining)
            {
                return Response<CreatePropertyResponseDto>.Fail(
                    string.Format(PropertyResponse.WillExceedMaxUploadCount, remaining),
                    409);
            }

            return Response<CreatePropertyResponseDto>.Ok(new CreatePropertyResponseDto(property.Id, property.Status));
        }
    }
}