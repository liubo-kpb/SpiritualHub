﻿namespace SpiritualHub.Data.Repository;

using System.Linq;

using Microsoft.EntityFrameworkCore;

using Interfaces;
using Data;

public class DeletableRepository<TEntity> : Repository<TEntity>, IDeletableRepository<TEntity>
    where TEntity : class
{
    public DeletableRepository(SpiritsDbContext context)
        : base(context)
    {

    }

    public void DeleteEntriesWithForeignKeys<TEntityType, TKey>(string foreignKeyColumnName, TKey entityId)
        where TEntityType : class
    {
        var context = Context.Set<TEntityType>();

        var relatedEntries = context.Where(e => Equals(EF.Property<TKey>(e, foreignKeyColumnName), entityId));
        context.RemoveRange(relatedEntries);
    }

    public virtual void Delete(TEntity entity)
    {
        DbSet.Remove(entity);
    }

    public virtual void DeleteMultiple(IEnumerable<TEntity> entities)
    {
        DbSet.RemoveRange(entities);
    }
}
