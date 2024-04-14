namespace SpiritualHub.Tests.Service.BusinessService.CategoryService;

using AutoMapper;
using Moq;

using Data.Models;
using Data.Repository.Interfaces;
using Services;
using Services.Interfaces;
using Services.Mappings;

public class MockConfiguration
{
    protected ICategoryService _categoryService;
    protected Mock<IDeletableRepository<Category>> _categoryRepositoryMock;

    private IMapper _mapper;

    [OneTimeSetUp]
    public void OneTimeSetup()
    {
        var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile<ApplicationProfile>());
        _mapper = new Mapper(mapperConfiguration);
    }

    [SetUp]
    public void Setup()
    {
        _categoryRepositoryMock = new Mock<IDeletableRepository<Category>>();
        _categoryService = new CategoryService(_categoryRepositoryMock.Object, _mapper);
    }
}
