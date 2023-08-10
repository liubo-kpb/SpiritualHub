namespace SpiritualHub.Data.Repository.Interface;

using System.Linq.Expressions;

public interface IRepository<TEntity> : IDisposable
        where TEntity : class
{
    IQueryable<TEntity> GetAll();

    Task<TEntity?> GetSingleByAsync(Expression<Func<TEntity, bool>> func);

    IQueryable<TEntity> AllAsNoTracking();

    Task<bool> AddAsync(TEntity entity);

    void Update(TEntity entity);

    Task<int> SaveChangesAsync();

    Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate);
}
