namespace KwikNesta.Shared.Contracts
{
    public interface IBaseRepositoryManager
    {
        Task SaveAsync();
        Task BeginTransaction(Func<Task> action);
    }
}