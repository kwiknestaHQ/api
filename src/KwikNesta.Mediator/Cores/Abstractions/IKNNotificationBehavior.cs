namespace KwikNesta.Mediator.Cores.Abstractions
{
    public interface IKNNotificationBehavior<TNotification> where TNotification : IKNNotification
    {
        Task HandleAsync(TNotification notification, CancellationToken cancellationToken, Func<Task> next);
    }
}