using Hangfire;
using Hangfire.Console;
using Hangfire.Server;
using KwikNesta.Mediator.Cores.Abstractions;
using KwikNesta.Shared.Extensions;
using KwikNesta.Shared.Helpers;
using KwikNesta.Shared.Implementations;
using KwikNesta.Shared.Models.Enumerations.Infra;
using KwikNesta.Shared.Models.Enumerations.Property;
using KwikNesta.Shared.Models.Settings;
using KwikNesta.Shared.Responses;
using KwikNesta.Shared.ServiceCommands.Property;
using KwikNesta.Shared.ServiceDTOs.Identity;
using KwikNesta.Shared.ServiceQueries.Identity;
using KwikNesta.Shared.ServiceQueries.Property;
using KwikNestaProperty.Domain.Entities;
using KwikNestaProperty.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace KwikNestaProperty.Application.Handlers
{
    public class ViewRequestApprovalCommandHandler(IPropertyRepositoryManager repository,
                                            IHostEnvironment host,
                                            IKNMediator mediator,
                                            IOptions<KNApplicationSettings> options) 
        : IKNRequestHandler<ViewRequestApprovalCommand, Response<string>>
    {
        private readonly IPropertyRepositoryManager _repository = repository;
        private readonly IHostEnvironment _host = host;
        private readonly IKNMediator _mediator = mediator;
        private readonly string _supportEmail = options.Value.AppAdmin.SupportEmail;
        private readonly string _clientBaseUrl = options.Value.AppAdmin.BaseUrl;
        private readonly string _secretKey = options.Value.Jwt.Key;

        public async Task<Response<string>> HandleAsync(ViewRequestApprovalCommand request, CancellationToken cancellationToken)
        {
            if(string.IsNullOrWhiteSpace(request.Token))
            {
                return Response<string>.Fail("Not authenticated", StatusCodes.Status403Forbidden);
            }

            var (requestId, propertyOwnerId, error) = TokenHelper.ValidateViewRequestToken(request.Token!, _secretKey);
            var loggedInUserId = !string.IsNullOrWhiteSpace(request.LoggedInUserId) ?
                request.LoggedInUserId : propertyOwnerId;

            var landlordResult = await _mediator.SendAsync(new GetUserByIdQuery
            {
                Id = loggedInUserId
            }, cancellationToken);

            if (!landlordResult.Success)
            {
                return Response<string>.Fail(landlordResult.Message, landlordResult.StatusCode);
            }

            var landlord = landlordResult.Data;
            var viewRequest = await _repository.ViewingRequest
                .FirstOrDefault(v => v.Id == request.RequestId && v.Property.OwnerId == landlord.Id, true);

            if (viewRequest == null)
            {
                return Response<string>.Fail(string.Format(PropertyResponse.RecordNotFound, "View Request"),
                    StatusCodes.Status404NotFound);
            }

            if(viewRequest.Status == EViewingStatus.Approved)
            {
                return Response<string>.Fail(PropertyResponse.ViewRequestAlreadyApproved,
                   StatusCodes.Status409Conflict);
            }

            viewRequest.Status = EViewingStatus.Approved;
            var session = new ViewingSession
            {
                ViewingRequestId = viewRequest.Id,
                ChannelName = AgoraChannelName.Generate(_host.EnvironmentName, 
                                                    EAgoraModule.Inspection, 
                                                    viewRequest.Id.ToString(),
                                                    _secretKey),
                ScheduledStart = viewRequest.RequestedDate
            };

            session.Participants.Add(new SessionParticipant
            {
                UserId = viewRequest.UserId,
                Role = ESessionParticipantRole.Subscriber,
                UId = viewRequest.UserId.ToUId()
            });

            session.Participants.Add(new SessionParticipant
            {
                UserId = landlord.Id,
                UId = landlord.Id.ToUId(),
                Role = ESessionParticipantRole.Publisher
            });

            await _repository.ViewingSession.AddAsync(session);

            viewRequest.SessionId = session.Id;
            await _repository.SaveAsync();

            BackgroundJob.Enqueue(() => NotifyUsers(viewRequest.Id, 
                                                viewRequest.PropertyId,
                                                landlord, 
                                                viewRequest.UserId, null!));

            BackgroundJob.Schedule(() => ScheduleReminder(viewRequest.Id,
                                                viewRequest.PropertyId,
                                                landlord,
                                                viewRequest.UserId, null!),
                                         viewRequest.RequestedDate.AddMinutes(-30));

            AppAudit.Write(landlord.Id,
                landlord.Email,
                EAuditAction.ViewRequestApproved,
                EAuditDomain.ViewRequest,
                viewRequest.Id.ToString(),
                request.IpAddress);

            return Response<string>.Ok(PropertyResponse.ViewRequestApproved);
        }

        public async Task NotifyUsers(Guid requestId, 
                            Guid propertyId, 
                            CurrentUserDto landlord, 
                            string requesterId, 
                            PerformContext context)
        {
            var viewRequestResult = await _mediator.SendAsync(new GetPropertyViewRequestQuery
            {
                UserId = landlord.Id,
                PropertyId = propertyId,
                RequestId = requestId
            });

            if (!viewRequestResult.Success)
            {
                context.WriteLine($"[ViewRequestApprovalCommandHandler] -> [NotifyUsers]: {viewRequestResult.Message}");
                return;
            }

            var requester = await _mediator.SendAsync(new GetUserByIdQuery
            {
                Id = requesterId
            });

            if (!requester.Success)
            {
                context.WriteLine($"[ViewRequestApprovalCommandHandler] -> [NotifyUsers]: {requester.Message}");
                return;
            }

            List<CurrentUserDto>? users = [landlord, requester.Data];
            var viewRequest = viewRequestResult.Data;
            if (!viewRequest.SessionId.HasValue)
            {
                context.WriteLine($"[ViewRequestApprovalCommandHandler] -> [NotifyUsers]: Invalid viewing session");
                return;
            }

            var isVirtual = viewRequest.Type == EViewingType.Virtual;
            var link = isVirtual ? 
                UrlHelpers.GetVirtualViewRequestJoinLink(_clientBaseUrl, viewRequest.SessionId.Value) : 
                UrlHelpers.GetPhysicalViewRequestCheckInLink(_clientBaseUrl, viewRequest.Id);


            foreach ( var user in users.WithProgress(context))
            {
                Notifications.SendEmail(user.Email,
                    PropertyResponse.ViewRequestSessionSubject,
                    _host.GetViewingSessionNotification(user.FirstName,
                                viewRequest.Property.Title,
                                _supportEmail, 
                                viewRequest.RequestedDate,
                                link,
                                isVirtual,
                                viewRequest.Property.Location));
            }
        }

        public async Task ScheduleReminder(Guid requestId, Guid propertyId, CurrentUserDto landlord, string requesterId, PerformContext context)
        {
            var viewRequestResult = await _mediator.SendAsync(new GetPropertyViewRequestQuery
            {
                UserId = landlord.Id,
                PropertyId = propertyId,
                RequestId = requestId
            });

            if (!viewRequestResult.Success)
            {
                context.WriteLine($"[ViewRequestApprovalCommandHandler] -> [ScheduleReminder]: {viewRequestResult.Message}");
                return;
            }

            var viewRequest = viewRequestResult.Data;
            if (viewRequest.Status == EViewingStatus.Cancelled)
            {
                context.WriteLine($"[ViewRequestApprovalCommandHandler] -> [ScheduleReminder]: Viewing Request Cancelled");
                return;
            }

            var requester = await _mediator.SendAsync(new GetUserByIdQuery
            {
                Id = requesterId
            });

            if (!requester.Success)
            {
                context.WriteLine($"[ViewRequestApprovalCommandHandler] -> [ScheduleReminder]: {requester.Message}");
                return;
            }

            List<CurrentUserDto>? users = [landlord, requester.Data];
            if (!viewRequest.SessionId.HasValue)
            {
                context.WriteLine($"[ViewRequestApprovalCommandHandler] -> [ScheduleReminder]: Invalid viewing session");
                return;
            }

            var isVirtual = viewRequest.Type == EViewingType.Virtual;
            var link = isVirtual ?
                UrlHelpers.GetVirtualViewRequestJoinLink(_clientBaseUrl, viewRequest.SessionId.Value) :
                UrlHelpers.GetPhysicalViewRequestCheckInLink(_clientBaseUrl, viewRequest.Id);


            foreach (var user in users.WithProgress(context))
            {
                Notifications.SendEmail(user.Email,
                    PropertyResponse.ViewRequestSessionReminderSubject,
                    _host.GetViewingSessionReminderNotification(user.FirstName,
                                _supportEmail,
                                link,
                                isVirtual));
            }
        }
    }
}