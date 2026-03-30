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
using KwikNestaProperty.Domain.Entities;
using KwikNestaProperty.Infrastructure;

namespace KwikNestaProperty.Application.Handlers
{
    public class SubmitVerificationCommandHandler(IPropertyRepositotyManager repository, 
                                                IUploadService uploadService) : IKNRequestHandler<SubmitVerificationCommand, Response<string>>
    {
        private readonly IPropertyRepositotyManager _repository = repository;
        private readonly IUploadService _uploadService = uploadService;

        public async Task<Response<string>> HandleAsync(SubmitVerificationCommand request, CancellationToken cancellationToken)
        {
            var validation = await Validate(request);
            if (!validation.Success)
            {
                return validation;
            }

            var property = await _repository.Property.FirstOrDefault(p => p.Id == request.PropertyId);
            if (property == null)
            {
                return Response<string>.Fail(
                    string.Format(PropertyResponse.RecordNotFound, "Property"),
                    404);
            }

            var fileTuples = new List<(byte[] Bytes, string Ext, string cType, EUploadType uType, EDocumentType dType, string? Other)>();
            foreach (var doc in request.Documents)
            {
                if(doc.DocumentType == EDocumentType.Other && string.IsNullOrWhiteSpace(doc.OtherDocumentType))
                {
                    return Response<string>.Fail(PropertyResponse.PleaseSpecifyForOther, 400);
                }

                var file = doc.File;
                var extension = Path.GetExtension(file.FileName).ToLower();
                var fileType = FileExtensions.GetFileType(extension);
                if (!file.IsValidFile(fileType))
                {
                    return Response<string>.Fail(PropertyResponse.Invalidfile, 400);
                }

                var bytes = file.GetBytes();
                var detectedType = bytes.DetectContentType();
                if (!FileValidation.IsValid(detectedType, extension))
                {
                    return Response<string>.Fail(PropertyResponse.UploadValidFiles, 400);
                }

                fileTuples.Add((bytes, extension, detectedType, fileType, doc.DocumentType, doc.OtherDocumentType));
            }

            BackgroundJob.Enqueue(()
                => ProcessVerificationRequest(request.PropertyId,
                        fileTuples,
                        request.UserContext.Id,
                        request.UserContext.Email,
                        request.UserContext.IpAddress,
                        null!));

            AppAudit.Write(request.UserContext.Id,
                    request.UserContext.Email,
                    EAuditAction.PropertyVerificationRequest,
                    EAuditDomain.Property,
                    property.Id.ToString(),
                    request.UserContext.IpAddress,
                    "Initiated Verification Request");

            return validation;
        }

        public async Task ProcessVerificationRequest(Guid propertyId,
                            List<(byte[] Bytes, string Ext, string cType, 
                                EUploadType uType, EDocumentType dType, string? Other)> payload,
                            string userId,
                            string userEmail,
                            string? userIpAddress,
                            PerformContext context)
        {
            var alreadyUploadedUrls = new List<string>();
            try
            {
                context.WriteLine("Media upload started for property: {0}", propertyId);
                var property = await _repository.Property.FirstOrDefault(p => p.Id == propertyId);
                if (property == null)
                {
                    context.WriteLine("No property record found for: {0}", propertyId);
                    return;
                }

                var ownershipRequest = new OwnershipVerificationRequest
                {
                    PropertyId = property.Id
                };

                var ownershipDocuments = new List<OwnershipDocument>();

                foreach (var (Bytes, Ext, cType, uType, dType, other) in payload)
                {
                    var url = await _uploadService.UploadFileAsync(Bytes, Ext, cType, uType);
                    alreadyUploadedUrls.Add(url);
                    ownershipDocuments.Add(new OwnershipDocument
                    {
                        VerificationRequestId = ownershipRequest.Id,
                        FileUrl = url,
                        DocumentType = dType,
                        OtherDocumentType = other
                    });
                }


                context.WriteLine("Adding {0} documents for {1}", ownershipDocuments.Count, property.Id);
                await _repository.BeginTransaction(async () =>
                {
                    await _repository.OwnershipVerification.AddAsync(ownershipRequest);
                    await _repository.OwnershipDocument.AddRangeAsync(ownershipDocuments);
                });

                AppAudit.Write(userId,
                    userEmail,
                    EAuditAction.PropertyVerificationRequest,
                    EAuditDomain.Property,
                    property.Id.ToString(),
                    userIpAddress,
                    "Completed Verification Request");

                context.WriteLine("Added {0} documents for {1}", ownershipDocuments.Count, property.Id);
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

        private async Task<Response<string>> Validate(SubmitVerificationCommand request)
        {
            if (request.PropertyId == Guid.Empty)
            {
                return Response<string>.Fail(PropertyResponse.InvalidRequest, 400);
            }

            if (!ValidationHelper.ValidUserContext(request.UserContext))
            {
                return Response<string>.Fail(PropertyResponse.UserNotAuthenticated, 403);
            }

            if (request.Documents.Count == 0)
            {
                return Response<string>.Fail(PropertyResponse.AtLeastOneDocRequired, 400);
            }

            var docs = request.Documents.Select(d => d.File).ToList();
            var hasDuplicates = await docs.HasDuplicates();
            if (hasDuplicates)
            {
                return Response<string>.Fail(PropertyResponse.DuplicatesDetectedInFile, 400);
            }

            if (!docs.AllowedFileTypesForVerification())
            {
                return Response<string>.Fail(PropertyResponse.UnsupportedFileType, 400);
            }

            if (!request.Documents.Any(d => d.DocumentType == EDocumentType.TitleDeed))
            {
                return Response<string>.Fail(PropertyResponse.TitleDeedRequired, 400);
            }

            var hasPending = await _repository.OwnershipVerification
                .ExistsAsync(x => x.PropertyId == request.PropertyId && x.Status == EVerificationStatus.Pending);

            if (hasPending)
            {
                return Response<string>.Fail(PropertyResponse.HasPendingVerificationRequest, 409);
            }

            return Response<string>.Ok(PropertyResponse.VerificationRequestSubmitted);
        }
    }
}