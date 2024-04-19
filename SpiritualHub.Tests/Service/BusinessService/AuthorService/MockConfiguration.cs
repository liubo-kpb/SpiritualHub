namespace SpiritualHub.Tests.Service.BusinessService.AuthorService;

using Microsoft.AspNetCore.Identity;

using Moq;
using AutoMapper;
using NuGet.Packaging;

using Data.Models;
using Data.Configuration.Seed;
using Data.Repository.Interfaces;
using Services;
using Services.Interfaces;
using Services.Mappings;

public abstract class MockConfiguration
{
    protected IAuthorService _authorService;
    protected Mock<IAuthorRepository> _authorRepositoryMock;
    protected Mock<IRepository<ApplicationUser>> _userRepositoryMock;
    protected Mock<UserManager<ApplicationUser>> _userManagerMock;

    protected List<Author> _authors = new List<Author>(new SeedAuthorConfiguration().GenerateEntities());
    protected List<Publisher> _publishers = new List<Publisher>(new SeedPublisherConfiguration().GenerateEntities());
    protected List<ApplicationUser> _users = new List<ApplicationUser>(new SeedUserConfiguration().GenerateEntities());

    protected IMapper _mapper;

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

    protected Author GetAuthorWithFollower(ApplicationUser testUser = null!)
    {
        testUser ??= _users.First();

        var testAuthor = _authors[0];
        testAuthor.Followers.Add(testUser);

        return testAuthor;
    }

    protected Author GetAuthorWithSubscriber(ApplicationUser testUser = null!)
    {
        testUser ??= _users[2];

        var testAuthor = _authors[3];
        var authorSubscriptionPlans = new SeedSubscriptionConfiguration().GenerateEntities().Where(s => s.AuthorID == testAuthor.Id);
        testAuthor.Subscriptions.AddRange(authorSubscriptionPlans);
        testAuthor.Subscriptions.First().Subscribers.Add(testUser);

        return testAuthor;
    }

    protected void ConnectPublishers()
    {
        _authors[0].Publishers.Add(_publishers[0]);
        _authors[2].Publishers.Add(_publishers[0]);
    }
}
