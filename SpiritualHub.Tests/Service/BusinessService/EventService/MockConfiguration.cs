namespace SpiritualHub.Tests.Service.BusinessService.EventService;

using Moq;
using AutoMapper;

using Data.Models;
using Data.Configuration.Seed;
using Data.Repository.Interfaces;
using Services;
using Services.Interfaces;
using Services.Mappings;

public class MockConfiguration
{
    protected IEventService _eventService;

    protected Mock<IEventRepository> _eventRepositoryMock;
    protected Mock<IDeletableRepository<Image>> _imageRepositoryMock;
    protected Mock<IDeletableRepository<Rating>> _ratingRepositoryMock;
    protected Mock<IRepository<ApplicationUser>> _userRepositoryMock;

    protected IMapper _mapper;

    protected List<Event> _events = null!;
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

        _eventRepositoryMock = new Mock<IEventRepository>();
        _imageRepositoryMock = new Mock<IDeletableRepository<Image>>();
        _ratingRepositoryMock = new Mock<IDeletableRepository<Rating>>();
        _userRepositoryMock = new Mock<IRepository<ApplicationUser>>();

        _eventService = new EventService(_eventRepositoryMock.Object, _imageRepositoryMock.Object, _ratingRepositoryMock.Object, _userRepositoryMock.Object, _mapper);
    }

    protected Event GetEventWithParticipant(ApplicationUser user = null!)
    {
        var course = _events.First();
        course.Participants.Add(user ?? _users.First());

        return course;
    }

    private void LoadEntities()
    {
        _events = new SeedEventConfiguration().GenerateEntities().ToList();
        _users = new SeedUserConfiguration().GenerateEntities().ToList();
    }
}
