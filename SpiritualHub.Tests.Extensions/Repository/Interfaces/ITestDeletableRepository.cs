namespace SpiritualHub.Tests.Extensions.Repository.Interfaces;

using SpiritualHub.Data.Repository.Interfaces;

public interface ITestDeletableRepository<TEntity> : IDeletableRepository<TEntity>
    where TEntity : class
{
    public int AddAsyncCounter { get; set; }

    public int SaveChangesAsyncCounter { get; set; }
}
