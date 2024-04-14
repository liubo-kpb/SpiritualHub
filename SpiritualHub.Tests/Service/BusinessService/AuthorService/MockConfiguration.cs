namespace SpiritualHub.Tests.Service.BusinessService.AuthorService;

using Microsoft.AspNetCore.Identity;

using AutoMapper;
using Moq;

using Data.Models;
using Data.Repository.Interfaces;
using Services;
using Services.Interfaces;
using Services.Mappings;

public class MockConfiguration
{
    protected IAuthorService _authorService;
    protected Mock<IAuthorRepository> _authorRepositoryMock;
    protected Mock<IRepository<ApplicationUser>> _userRepositoryMock;
    protected Mock<UserManager<ApplicationUser>> _userManagerMock;

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
        _authorRepositoryMock = new Mock<IAuthorRepository>();
        _userRepositoryMock = new Mock<IRepository<ApplicationUser>>();
        _userManagerMock = new Mock<UserManager<ApplicationUser>>();

        _authorService = new AuthorService(_authorRepositoryMock.Object, _userRepositoryMock.Object, _mapper, _userManagerMock.Object);
    }
}
