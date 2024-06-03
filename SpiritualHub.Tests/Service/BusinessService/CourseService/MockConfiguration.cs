namespace SpiritualHub.Tests.Service.BusinessService.CourseService;

using Moq;
using AutoMapper;

using Services;
using Services.Mappings;
using Services.Interfaces;
using Data.Models;
using Data.Repository.Interfaces;
using Data.Configuration.Seed;

public class MockConfiguration
{
    protected ICourseService _courseService;
    protected Mock<IModuleService> _moduleServiceMock;

    protected Mock<ICourseRepository> _courseRepositoryMock;
    protected Mock<IDeletableRepository<Image>> _imageRepositoryMock;
    protected Mock<IDeletableRepository<Rating>> _ratingRepositoryMock;
    protected Mock<IRepository<ApplicationUser>> _userRepositoryMock;

    protected IMapper _mapper;

    protected List<Course> _courses = null!;
    protected List<Author> _authors = null!;
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

        _moduleServiceMock = new Mock<IModuleService>();

        _courseRepositoryMock = new Mock<ICourseRepository>();
        _imageRepositoryMock = new Mock<IDeletableRepository<Image>>();
        _ratingRepositoryMock = new Mock<IDeletableRepository<Rating>>();
        _userRepositoryMock = new Mock<IRepository<ApplicationUser>>();

        _courseService = new CourseService(_courseRepositoryMock.Object, _moduleServiceMock.Object, _imageRepositoryMock.Object, _ratingRepositoryMock.Object, _userRepositoryMock.Object, _mapper);
    }

    protected Course GetCourseWithStudent(ApplicationUser user = null!)
    {
        var course = _courses.First();
        course.Students.Add(user ?? _users.First());

        return course;
    }

    private void LoadEntities()
    {
        _courses = new SeedCourseConfiguration().GenerateEntities().ToList();
        _authors = new SeedAuthorConfiguration().GenerateEntities().ToList();
        _users = new SeedUserConfiguration().GenerateEntities().ToList();
    }
}
