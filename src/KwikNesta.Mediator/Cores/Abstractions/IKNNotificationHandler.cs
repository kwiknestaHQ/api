namespace KwikNesta.Mediator.Cores.Abstractions
{
    public interface IKNNotificationHandler<TNotification> where TNotification : IKNNotification
    {
        Task HandleAsync(TNotification notification, CancellationToken cancellationToken);
    }
}