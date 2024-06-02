namespace SpiritualHub.Tests.Service.BusinessService.BookService;

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
    protected IBookService _bookService;
    protected Mock<IBookRepository> _bookRepositoryMock;
    protected Mock<IDeletableRepository<Image>> _imageRepositoryMock;
    protected Mock<IDeletableRepository<Rating>> _ratingRepositoryMock;
    protected Mock<IRepository<ApplicationUser>> _userRepositoryMock;
    protected IMapper _mapper;

    protected List<Book> _books = null!;
    protected List<Author> _authors = null!;
    protected List<ApplicationUser> _users = null!;

    protected bool GenerateEntities { get; set; } = false;

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

        _bookRepositoryMock = new Mock<IBookRepository>();
        _imageRepositoryMock = new Mock<IDeletableRepository<Image>>();
        _ratingRepositoryMock = new Mock<IDeletableRepository<Rating>>();
        _userRepositoryMock = new Mock<IRepository<ApplicationUser>>();

        _bookService = new BookService(_bookRepositoryMock.Object, _imageRepositoryMock.Object, _ratingRepositoryMock.Object, _userRepositoryMock.Object, _mapper);
    }

    protected Book GetBookWithReader(ApplicationUser user = null!)
    {
        var book = _books.First();
        book.Readers.Add(user ?? _users.First());

        return book;
    }

    private void LoadEntities()
    {
        _books = new SeedBookConfiguration().GenerateEntities().ToList();
        _authors = new SeedAuthorConfiguration().GenerateEntities().ToList();
        _users = new SeedUserConfiguration().GenerateEntities().ToList();
    }
}
