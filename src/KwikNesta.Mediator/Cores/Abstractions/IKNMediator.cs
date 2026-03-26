namespace KwikNesta.Mediator.Cores.Abstractions
{
    public interface IKNMediator
    {
        Task<TResponse> SendAsync<TResponse>(IKNRequest<TResponse> request, CancellationToken cancellationToken = default);
        Task PublishAsync<TNotification>(TNotification notification, CancellationToken cancellationToken = default) where TNotification : IKNNotification;
    }
}
