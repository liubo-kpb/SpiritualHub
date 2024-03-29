namespace SpiritualHub.Tests;

using Microsoft.EntityFrameworkCore;

using Data;
using SpiritualHub.Services.Mappings;
using AutoMapper;

/// <summary>
/// Class has Setup method creating and initializing InMemory Database, <see cref="SeedTestData(Data.SpiritsDbContext)"/> and <see cref="InitializeServices(Data.SpiritsDbContext)"/> abstract methods.
/// </summary>
public abstract class TestBaseSetup<TServiceType>
    where TServiceType : class
{
    protected SpiritsDbContext DbContext { get; set; } = null!;

    protected TServiceType Service { get; set; } = null!;

    protected abstract TServiceType InitializeServices(IMapper mapper);

    protected abstract void SeedTestData();

    [SetUp]
    protected virtual void Setup()
    {
        var options = new DbContextOptionsBuilder<SpiritsDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDb")
            .Options;

        DbContext = new SpiritsDbContext(options);

        SeedTestData();
        DbContext.SaveChanges();

        var mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile<ApplicationProfile>());
        var mapper = new Mapper(mapperConfig);

        Service = InitializeServices(mapper);
    }
}