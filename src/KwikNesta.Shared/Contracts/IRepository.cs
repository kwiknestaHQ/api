using System.Linq.Expressions;

namespace KwikNesta.Shared.Contracts
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);
        Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate);
        Task AddAsync(TEntity entity);
        void Remove(TEntity entity);
        void Update(TEntity entity);
        Task<TEntity?> FirstOrDefault(Expression<Func<TEntity, bool>> predicate, bool track = false);
        IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> predicate);
        void RemoveMany(IEnumerable<TEntity> entities);
        Task AddRangeAsync(List<TEntity> entities);
    }
}