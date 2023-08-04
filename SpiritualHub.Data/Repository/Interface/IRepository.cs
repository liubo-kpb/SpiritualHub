namespace SpiritualHub.Data.Repository.Interface;

using System.Linq.Expressions;

public interface IRepository<TEntity> : IDisposable
        where TEntity : class
{
    Task<IQueryable<TEntity>> GetAllAsync();

    Task<TEntity?> GetSingleAsync(Guid? id);

    Task<IQueryable<TEntity>> AllAsNoTrackingAsync();

    Task<bool> AddAsync(TEntity entity);

    void Update(TEntity entity);

    Task<int> SaveChangesAsync();

    Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate);
}
