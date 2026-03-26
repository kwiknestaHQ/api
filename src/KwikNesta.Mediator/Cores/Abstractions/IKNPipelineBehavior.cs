namespace KwikNesta.Mediator.Cores.Abstractions
{
    public interface IKNPipelineBehavior<TRequest, TResponse>
    {
        Task<TResponse> HandleAsync(TRequest request, CancellationToken cancellationToken, Func<Task<TResponse>> next);
    }
}