namespace SpiritualHub.Tests.Service.BusinessService.BookService;

using Moq;
using AutoMapper;

using Services;
using Services.Mappings;
using Services.Interfaces;
using Data.Repository.Interfaces;
using Data.Models;

public class MockConfiguration
{
    protected IBookService _bookService;
    protected Mock<IBookRepository> _bookRepositoryMock;
    protected Mock<IDeletableRepository<Image>> _imageRepositoryMock;
    protected Mock<IDeletableRepository<Rating>> _ratingRepositoryMock;
    protected Mock<IRepository<ApplicationUser>> _userRepositoryMock;
    protected IMapper _mapper;



    [OneTimeSetUp]
    public virtual void OneTimeSetup()
    {
        var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile<ApplicationProfile>());
        _mapper = new Mapper(mapperConfiguration);
    }

    [SetUp]
    public virtual void Setup()
    {
        _bookRepositoryMock = new Mock<IBookRepository>();
        _imageRepositoryMock = new Mock<IDeletableRepository<Image>>();
        _ratingRepositoryMock = new Mock<IDeletableRepository<Rating>>();
        _userRepositoryMock = new Mock<IRepository<ApplicationUser>>();

        _bookService = new BookService(_bookRepositoryMock.Object, _imageRepositoryMock.Object, _ratingRepositoryMock.Object, _userRepositoryMock.Object, _mapper);
    }
}
