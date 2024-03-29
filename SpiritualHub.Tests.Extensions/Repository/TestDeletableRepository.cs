namespace SpiritualHub.Tests.Extensions.Repository;

using System.Threading.Tasks;

using Interfaces;
using Data;
using Data.Repository;

public class TestDeletableRepository<TEntity> : DeletableRepository<TEntity>, ITestDeletableRepository<TEntity>
    where TEntity : class
{
    public TestDeletableRepository(SpiritsDbContext context) : base(context)
    {
    }

    public int AddAsyncCounter { get; set; }

    public int SaveChangesAsyncCounter { get; set; }

    public override Task<bool> AddAsync(TEntity entity)
    {
        AddAsyncCounter++;

        return base.AddAsync(entity);
    }

    public override Task<int> SaveChangesAsync()
    {
        SaveChangesAsyncCounter++;

        return base.SaveChangesAsync();
    }
}
