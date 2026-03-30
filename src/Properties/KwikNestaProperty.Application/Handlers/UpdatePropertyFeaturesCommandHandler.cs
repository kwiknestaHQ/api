using KwikNesta.Mediator.Cores.Abstractions;
using KwikNesta.Shared.Extensions;
using KwikNesta.Shared.Implementations;
using KwikNesta.Shared.Models.Enumerations.Infra;
using KwikNesta.Shared.Responses;
using KwikNesta.Shared.ServiceCommands.Property;
using KwikNesta.Shared.ServiceDTOs.Property;
using KwikNestaProperty.Application.Validations;
using KwikNestaProperty.Domain.Entities;
using KwikNestaProperty.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace KwikNestaProperty.Application.Handlers
{
    public class UpdatePropertyFeaturesCommandHandler(IPropertyRepositotyManager repository) 
        : IKNRequestHandler<UpdatePropertyFeaturesCommand, Response<CreatePropertyResponseDto>>
    {
        private readonly IPropertyRepositotyManager _repository = repository;

        public async Task<Response<CreatePropertyResponseDto>> HandleAsync(UpdatePropertyFeaturesCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdatePropertyFeaturesCommandValidator().Validate(request);
            if (!validator.IsValid)
            {
                return Response<CreatePropertyResponseDto>.Fail(validator.Errors.FirstOrDefault()?.ErrorMessage ??
                PropertyResponse.InvalidRequest, 400);
            }

            var property = await _repository.Property.FirstOrDefault(p => p.Id == request.PropertyId);
            if (property == null)
            {
                return Response<CreatePropertyResponseDto>.Fail(string.Format(PropertyResponse.RecordNotFound, "Property"), 404);
            }

            var normalizedCustoms = request.CustomFeatures
                .Where(f => !string.IsNullOrWhiteSpace(f))
                .Select(f => f.Trim())
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .ToList();

            var validFeatureIds = await _repository.PropertyFeature
                .Get(f => request.FeatureIds.Contains(f.Id))
                .Select(f => f.Id)
                .ToListAsync(cancellationToken);

            if (validFeatureIds.Count != request.FeatureIds.Count)
            {
                return Response<CreatePropertyResponseDto>.Fail(PropertyResponse.InvalidSelectedFeatures, 400);
            }

            await _repository.BeginTransaction(async () =>
            {
                // 1. Remove existing
                var existing = _repository.PropertyFeatureLink
                    .Get(x => x.PropertyId == request.PropertyId);

                _repository.PropertyFeatureLink.RemoveMany(existing);

                // 2. Add predefined features
                var featureLinks = request.FeatureIds.Select(f => new PropertyFeatureLink
                {
                    PropertyId = request.PropertyId,
                    FeatureId = f,
                    CustomFeature = null,
                    CustomFeatureNormalized = null
                }).ToList();

                await _repository.PropertyFeatureLink.AddRangeAsync(featureLinks);

                // 3. Add custom features
                var customLinks = normalizedCustoms.Select(f => new PropertyFeatureLink
                {
                    PropertyId = request.PropertyId,
                    FeatureId = null,
                    CustomFeature = f.CapitalizeEachWord(),
                    CustomFeatureNormalized = f.ToLower()
                }).ToList();

                await _repository.PropertyFeatureLink.AddRangeAsync(customLinks);
            });

            AppAudit.Write(request.UserContext.Id,
                request.UserContext.Email!,
                EAuditAction.AddOrUpdatePropertyFeature,
                EAuditDomain.Property,
                property.Id.ToString(),
                request.UserContext.IpAddress);

            return Response<CreatePropertyResponseDto>.Ok(new CreatePropertyResponseDto(property.Id, property.Status));
        }
    }
}