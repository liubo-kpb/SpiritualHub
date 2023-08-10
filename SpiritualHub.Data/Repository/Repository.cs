namespace SpiritualHub.Data.Repository;

using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using Interface;
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

    public virtual IQueryable<TEntity> GetAll()
    {
        return DbSet;
    }

    public virtual async Task<TEntity?> GetSingleByAsync(Expression<Func<TEntity, bool>> func)
    {
        return await DbSet.FindAsync(func);
    }

    public virtual IQueryable<TEntity> AllAsNoTracking()
    {
        return DbSet.AsNoTracking();
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
