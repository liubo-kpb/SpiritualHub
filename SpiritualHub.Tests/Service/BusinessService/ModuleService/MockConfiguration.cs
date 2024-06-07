namespace SpiritualHub.Tests.Service.BusinessService.ModuleService;

using Moq;
using AutoMapper;

using Services;
using Services.Interfaces;
using Services.Mappings;
using Data.Configuration.Seed;
using Data.Models;
using Data.Repository.Interfaces;

public class MockConfiguration
{
    protected IModuleService _moduleService;

    protected Mock<IModuleRepository> _moduleRepositoryMock;
    protected Mock<ICourseRepository> _courseRepositoryMock;
    protected IMapper _mapper;

    protected List<Module> _modules = null!;
    protected List<ApplicationUser> _users = null!;

    protected bool GenerateEntities { get; set; } = true;

    [OneTimeSetUp]
    public virtual void OneTimeSetup()
    {
        var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile<ApplicationProfile>());
        _mapper = new Mapper(mapperConfiguration);
    }

    [SetUp]
    public virtual void Setup()
    {
        if (GenerateEntities)
        {
            LoadEntities();
        }

        _moduleRepositoryMock = new Mock<IModuleRepository>();
        _courseRepositoryMock = new Mock<ICourseRepository>();

        _moduleService = new ModuleService(_moduleRepositoryMock.Object, _courseRepositoryMock.Object, _mapper);
    }

    private void LoadEntities()
    {
        _modules = new SeedModuleConfiguration().GenerateEntities().ToList();
        _users = new SeedUserConfiguration().GenerateEntities().ToList();
    }
}
