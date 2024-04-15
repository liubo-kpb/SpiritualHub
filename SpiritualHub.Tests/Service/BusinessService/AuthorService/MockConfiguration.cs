namespace SpiritualHub.Tests.Service.BusinessService.AuthorService;

using Microsoft.AspNetCore.Identity;

using AutoMapper;
using Moq;

using Data.Models;
using Data.Repository.Interfaces;
using Services;
using Services.Interfaces;
using Services.Mappings;
using SpiritualHub.Data.Configuration.Seed;

public abstract class MockConfiguration
{
    protected IAuthorService _authorService;
    protected Mock<IAuthorRepository> _authorRepositoryMock;
    protected Mock<IRepository<ApplicationUser>> _userRepositoryMock;
    protected Mock<UserManager<ApplicationUser>> _userManagerMock;

    protected List<Author> _authors = new List<Author>(new SeedAuthorConfiguration().GenerateEntities());
    protected List<Publisher> _publishers = new List<Publisher>(new SeedPublisherConfiguration().GenerateEntities());
    protected List<ApplicationUser> _users = new List<ApplicationUser>(new SeedUserConfiguration().GenerateEntities());

    private IMapper _mapper;

    [OneTimeSetUp]
    public void OneTimeSetup()
    {
        var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile<ApplicationProfile>());
        _mapper = new Mapper(mapperConfiguration);
    }

    [SetUp]
    public virtual void Setup()
    {
        _authorRepositoryMock = new Mock<IAuthorRepository>();
        _userRepositoryMock = new Mock<IRepository<ApplicationUser>>();
        _userManagerMock = new Mock<UserManager<ApplicationUser>>(
            Mock.Of<IUserStore<ApplicationUser>>(),
            null!, null!, null!, null!, null!, null!, null!, null!
        ); ;

        _authorService = new AuthorService(_authorRepositoryMock.Object, _userRepositoryMock.Object, _mapper, _userManagerMock.Object);
    }
}
