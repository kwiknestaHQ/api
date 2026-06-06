using KwikNesta.Mediator.Cores.Abstractions;
using KwikNesta.Shared.Implementations;
using KwikNesta.Shared.Models.Enumerations.Infra;
using KwikNesta.Shared.Models.Enumerations.Payments;
using KwikNesta.Shared.Models.Enumerations.Property;
using KwikNesta.Shared.Models.Settings;
using KwikNesta.Shared.Responses;
using KwikNesta.Shared.ServiceCommands.Property;
using KwikNesta.Shared.ServiceDTOs.Property;
using KwikNesta.Shared.ServiceQueries.Payment;
using KwikNestaProperty.Application.Validations;
using KwikNestaProperty.Domain.Entities;
using KwikNestaProperty.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace KwikNestaProperty.Application.Handlers
{
    public class CreateViewingRequestCommandHandler(IPropertyRepositoryManager repository, 
                                                IKNMediator mediator,
                                                IOptions<KNApplicationSettings> options) 
        : IKNRequestHandler<CreateViewingRequestCommand, Response<ViewingRequestResult>>
    {
        private readonly IPropertyRepositoryManager _repository = repository;
        private readonly IKNMediator _mediator = mediator;
        private readonly KNAdminSettings _adminSettings = options.Value.AppAdmin;

        public async Task<Response<ViewingRequestResult>> HandleAsync(CreateViewingRequestCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateViewingRequestCommandValidator().Validate(request);
            if (!validator.IsValid)
            {
                return Response<ViewingRequestResult>.Fail(validator.Errors.FirstOrDefault()?.ErrorMessage ??
                    PropertyResponse.InvalidRequest, StatusCodes.Status400BadRequest);
            }

            var duration = TimeSpan.FromMinutes(_adminSettings.ViewSessionDurationMinutes);
            var newStart = request.ScheduledDate;
            var newEnd = newStart.Add(duration);
            var adjustedStart = newStart - duration;

            var conflict = await _repository.ViewingRequest.ExistsAsync(v =>
                v.PropertyId == request.PropertyId &&
                v.Status == EViewingStatus.Approved &&
                v.RequestedDate < newEnd &&
                v.RequestedDate > adjustedStart
            );

            if (conflict)
            {
                return Response<ViewingRequestResult>.Fail(PropertyResponse.ConflictingViewRequest, 
                    StatusCodes.Status409Conflict);
            }

            var property = await _repository.Property
                .FirstOrDefault(p => p.Id == request.PropertyId);
            if(property == null)
            {
                return Response<ViewingRequestResult>.Fail(string.Format(PropertyResponse.RecordNotFound, "Property"), 
                    StatusCodes.Status404NotFound);
            }

            var feeRule = await _mediator.SendAsync(new FeeRuleQuery
            {
                Amount = property.Price,
                AppliesTo = EPaymentPurpose.Viewing,
                Type = EFeeType.Charge,
            }, cancellationToken);

            if (!feeRule.Success)
            {
                return Response<ViewingRequestResult>.Fail(feeRule.Message, feeRule.StatusCode);
            }

            var viewRequest = new ViewingRequest
            {
                PropertyId = request.PropertyId,
                Fee = feeRule.Data.FinalAmount,
                RequestedDate = request.ScheduledDate,
                Type = request.Type,
                UserId = request.Context.Id,
                Note = request.Note,
            };

            await _repository.ViewingRequest.AddAsync(viewRequest);
            await _repository.SaveAsync();

            AppAudit.Write(request.Context.Id,
                request.Context.Email,
                EAuditAction.ViewRequested,
                EAuditDomain.ViewRequest,
                viewRequest.Id.ToString(),
                request.Context.IpAddress);

            return Response<ViewingRequestResult>.Ok(new ViewingRequestResult(viewRequest.Id, viewRequest.Fee));
        }
    }
}