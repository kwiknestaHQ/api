using Hangfire;
using KwikNesta.Mediator.Cores.Abstractions;
using KwikNesta.Mediator.Hangfire.Abstractions;
using Microsoft.Extensions.Logging;

namespace KwikNesta.Mediator.Hangfire.Implementations
{
    public class KNBackgroundMediator(IBackgroundJobClient backgroundJobClient,
                               ILogger<KNBackgroundMediator> logger) 
        : IKNBackgroundMediator
    {
        private readonly IBackgroundJobClient _backgroundJobClient = backgroundJobClient;
        private readonly ILogger<KNBackgroundMediator> _logger = logger;

        public void Send<TResponse>(IKNRequest<TResponse> request)
        {
            try
            {
                _logger.LogInformation("Enqueuing background Send for {Request}", request.GetType().Name);
                _backgroundJobClient.Enqueue<IKNMediator>(m => m.SendAsync(request, default));
                _logger.LogInformation("Enqueued background job for {Type}", request.GetType().Name);
                return;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to enqueue background job for {Type}", request.GetType().Name);
                throw;
            }
        }

        public void Publish<TNotification>(TNotification notification)
            where TNotification : IKNNotification
        {
            try
            {
                _logger.LogInformation("Enqueuing background Publish for {Notification}", notification.GetType().Name);
                _backgroundJobClient.Enqueue<IKNMediator>(m => m.PublishAsync(notification, default));
                _logger.LogInformation("Enqueued background Publish for {Notification}", notification.GetType().Name);
                return;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to enqueue background job for {Type}", notification.GetType().Name);
                throw;
            }
        }
    }
}