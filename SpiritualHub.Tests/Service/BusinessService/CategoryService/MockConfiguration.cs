namespace SpiritualHub.Tests.Service.BusinessService.CategoryService;

using AutoMapper;
using Moq;

using Data.Models;
using Data.Repository.Interfaces;
using Services;
using Services.Interfaces;

public class MockConfiguration
{
    protected ICategoryService _categoryService;
    protected Mock<IDeletableRepository<Category>> _categoryRepositoryMock;
    protected Mock<IMapper> _mapperMock;

    [SetUp]
    public void Setup()
    {
        _categoryRepositoryMock = new Mock<IDeletableRepository<Category>>();
        _mapperMock = new Mock<IMapper>();

        _categoryService = new CategoryService(_categoryRepositoryMock.Object, _mapperMock.Object);
    }
}
