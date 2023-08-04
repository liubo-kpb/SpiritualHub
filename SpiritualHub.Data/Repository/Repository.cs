namespace SpiritualHub.Data.Repository;

using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

using Interface;
using Microsoft.EntityFrameworkCore;
using SpiritualHub.Data;

public class Repository<TEntity> : IRepository<TEntity>
        where TEntity : class
{
    public Repository(SpiritsDbContext context)
    {
        this.Context = context ?? throw new ArgumentNullException(nameof(context));
        this.DbSet = Context.Set<TEntity>();
    }

    protected DbSet<TEntity> DbSet { get; set; }

    protected SpiritsDbContext Context { get; set; }

    public virtual async Task<IQueryable<TEntity>> GetAllAsync()
    {
        return new List<TEntity>(await DbSet.ToArrayAsync())
                                .AsQueryable();
    }

    public virtual async Task<TEntity?> GetSingleAsync(Guid? id)
    {
        return await DbSet.FindAsync(id);
    }

    public virtual async Task<IQueryable<TEntity>> AllAsNoTrackingAsync()
    {
        return new List<TEntity>(await DbSet.ToArrayAsync())
                                .AsQueryable()
                                .AsNoTracking();
    }

    public virtual async Task<bool> AddAsync(TEntity entity)
    {
        await DbSet.AddAsync(entity).AsTask();
        return true;
    }

    public virtual void Update(TEntity entity)
    {
        var entry = Context.Entry(entity);
        if (entry.State == EntityState.Detached)
        {
            DbSet.Attach(entity);
        }

        entry.State = EntityState.Modified;
    }

    public async Task<int> SaveChangesAsync() => await Context.SaveChangesAsync();

    public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await DbSet.AnyAsync(predicate);
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            Context.Dispose();
        }
    }
}
