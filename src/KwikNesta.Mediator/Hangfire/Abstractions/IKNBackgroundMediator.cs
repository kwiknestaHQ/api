using KwikNesta.Mediator.Cores.Abstractions;

namespace KwikNesta.Mediator.Hangfire.Abstractions
{
    public interface IKNBackgroundMediator
    {
        void Publish<TNotification>(TNotification notification) where TNotification : IKNNotification;
        void Send<TResponse>(IKNRequest<TResponse> request);
    }
}