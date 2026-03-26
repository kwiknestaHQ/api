namespace KwikNesta.Mediator.Cores.Abstractions
{
    public interface IKNRequestHandler<TRequest, TResponse> where TRequest : IKNRequest<TResponse>
    {
        Task<TResponse> HandleAsync(TRequest request, CancellationToken cancellationToken);
    }
}