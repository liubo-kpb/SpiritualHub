namespace SpiritualHub.Data.Repository.Interface;

public interface IDeletableRepository<TEntity> : IRepository<TEntity>
            where TEntity : class
{
    void Delete(TEntity entity);

    void DeleteEntriesWithForeignKeys<TEntityType, TKey>(string foreignKeyColumnName, TKey entityId)
        where TEntityType : class;

    void DeleteMultiple(IEnumerable<TEntity> entities);
}
