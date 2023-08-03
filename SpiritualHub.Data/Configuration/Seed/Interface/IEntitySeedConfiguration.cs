namespace SpiritualHub.Data.Configuration.Seed.Interface;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public interface IEntitySeedConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : class
{
    TEntity[] GenerateEntities();
}
