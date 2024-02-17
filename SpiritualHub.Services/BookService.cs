namespace SpiritualHub.Services;

using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

using AutoMapper;
using Mappings;

using Data.Repository.Interface;
using Data.Models;
using Interfaces;
using Models.Book;
using Client.ViewModels.Book;
using Client.Infrastructure.Enums;

public class BookService : IBookService
{
    private readonly IBookRepository _bookRepository;
    private readonly IDeletableRepository<Image> _imageRepository;
    private readonly IDeletableRepository<Rating> _ratingRepository;
    private readonly IRepository<ApplicationUser> _userRepository;
    private readonly IMapper _mapper;

    public BookService(
        IBookRepository bookRepository,
        IDeletableRepository<Image> imageRepository,
        IDeletableRepository<Rating> ratingRepository,
        IRepository<ApplicationUser> userRepository,
        IMapper mapper)
    {
        _bookRepository = bookRepository;
        _imageRepository = imageRepository;
        _ratingRepository = ratingRepository;
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task GetAsync(string bookId, string userId)
    {
        var book = await _bookRepository.GetSingleByIdAsync(bookId);
        var user = await _userRepository.GetSingleByIdAsync(userId);

        user!.Books.Add(book!);
        await _bookRepository.SaveChangesAsync();
    }

    public async Task<IEnumerable<BookViewModel>> AllBooksByUserIdAsync(string userId)
    {
        var book = await _bookRepository
            .AllAsNoTracking()
            .Include(b => b.Author)
            .Include(b => b.Image)
            .Include(b => b.Readers)
            .Where(b => b.Readers.Any(u => u.Id.ToString() == userId))
            .ToListAsync();

        var booksModel = new List<BookViewModel>();
        _mapper.MapListToViewModel(book, booksModel);

        for (int i = 0; i < booksModel.Count; i++)
        {
            booksModel[i].HasBook = true;
        }

        return booksModel;
    }

    public async Task<string> CreateAsync(BookFormModel newBook)
    {
        var newBookEntity = _mapper.Map<Book>(newBook);
        newBookEntity.Image.Name = newBook.Title;

        await _bookRepository.AddAsync(newBookEntity);
        await _bookRepository.SaveChangesAsync();

        return newBookEntity.Id.ToString();
    }

    public async Task DeleteAsync(string bookId)
    {
        var book = await _bookRepository.GetBookWithImageAndRatingsAsync(bookId);

        _bookRepository.Delete(book!);
        _imageRepository.Delete(book!.Image);
        _ratingRepository.DeleteMultiple(book!.Ratings);

        await _bookRepository.SaveChangesAsync();
    }

    public async Task EditAsync(BookFormModel updatedBook)
    {
        var book = await _bookRepository.GetBookInfoAsync(updatedBook.Id!);

        book!.Title = updatedBook.Title;
        book!.Description = updatedBook.Description;
        book!.ShortDescription = updatedBook.ShortDescription;
        book!.Price = updatedBook.Price;
        book!.IsHidden = updatedBook.IsHidden;
        book!.Image.URL = updatedBook.ImageUrl;
        book!.CategoryID = updatedBook.CategoryId;
        book!.AuthorID = Guid.Parse(updatedBook.AuthorId!);
        book!.PublisherID = Guid.Parse(updatedBook.PublisherId!);

        await _bookRepository.SaveChangesAsync();
    }

    public Task<bool> ExistsAsync(string id)
    {
        return _bookRepository.AnyAsync(b => b.Id.ToString() == id);
    }

    public async Task<FilteredBooksServiceModel> GetAllAsync(AllBooksQueryModel queryModel, string userId)
    {
        var booksQuery = _bookRepository
                            .AllAsNoTracking()
                            .Where(b => !b.IsHidden);

        if (!string.IsNullOrWhiteSpace(queryModel.CategoryName))
        {
            booksQuery = booksQuery.Where(b => b.Category != null && b.Category!.Name == queryModel.CategoryName);
        }

        if (!string.IsNullOrWhiteSpace(queryModel.SearchTerm))
        {
            string wildCard = $"%{queryModel.SearchTerm.ToLower()}%";
            booksQuery = booksQuery.Where(b => EF.Functions.Like(b.Title, wildCard)
                                            || EF.Functions.Like(b.Author.Name, wildCard)
                                            || EF.Functions.Like(b.Description, wildCard)
                                            || EF.Functions.Like(b.ShortDescription, wildCard));
        }

        booksQuery = queryModel.SortingOption switch
        {
            BookSorting.TopRated => booksQuery.OrderByDescending(b => b.Ratings.Count == 0 ? 0 : (b.Ratings.Sum(r => r.Stars) / (double) b.Ratings.Count)),
            BookSorting.LeastRated => booksQuery.OrderBy(b => b.Ratings.Count == 0 ? 0 : (b.Ratings.Sum(r => r.Stars) / (double) b.Ratings.Count)),
            BookSorting.Newest => booksQuery.OrderByDescending(b => b.AddedOn),
            BookSorting.Oldest => booksQuery.OrderBy(b => b.AddedOn),
            BookSorting.PriceAscending => booksQuery.OrderBy(b => b.Price),
            BookSorting.PriceDescending => booksQuery.OrderByDescending(b => b.Price),
            _ => booksQuery.OrderByDescending(b => b.AddedOn)
                           .ThenByDescending(b => b.Ratings.Count == 0 ? 0 : (b.Ratings.Sum(r => r.Stars) / (b.Ratings.Count * 1.0)))
        };

        var books = await booksQuery
                            .Skip((queryModel.CurrentPage - 1) * queryModel.EntitiesPerPage)
                            .Take(queryModel.EntitiesPerPage)
                            .Include(b => b.Image)
                            .Include(b => b.Author)
                            .Include(b => b.Readers)
                            .ToListAsync();

        List<BookViewModel> booksModel = new List<BookViewModel>();
        _mapper.MapListToViewModel(books, booksModel);

        for (int i = 0; i < booksModel.Count; i++)
        {
            booksModel[i].HasBook = SetHasBook(userId, books[i]);
        }

        return new FilteredBooksServiceModel()
        {
            Books = booksModel,
            TotalBooksCount = booksQuery.Count()
        };
    }

    public async Task<int> GetAllCountAsync()
    {
        return await _bookRepository
                            .AllAsNoTracking()
                            .CountAsync();
    }

    public async Task<string> GetAuthorIdAsync(string bookId)
    {
        return (await _bookRepository.GetBookWithAuthorAsync(bookId))!.AuthorID.ToString();
    }

    public async Task<BookDetailsViewModel> GetBookDetailsAsync(string id, string userId)
    {
        var bookEntity = await _bookRepository.GetFullBookDetailsAsync(id);
        var bookModel = _mapper.Map<BookDetailsViewModel>(bookEntity);
        bookModel.HasBook = SetHasBook(userId, bookEntity);

        return bookModel;
    }

    public async Task<BookFormModel> GetBookInfoAsync(string id)
    {
        var book = await _bookRepository.GetBookInfoAsync(id);
        var bookModel = _mapper.Map<BookFormModel>(book);

        return bookModel;
    }

    public async Task<IEnumerable<BookViewModel>> GetBooksByPublisherIdAsync(string publisherId, string userId)
    {
        var books = await _bookRepository
                            .AllAsNoTracking()
                            .Include(b => b.Image)
                            .Include(b => b.Author)
                            .Include(b => b.Readers)
                            .Where(b => b.PublisherID.ToString() == publisherId)
                            .ToListAsync();

        var booksModel = new List<BookViewModel>();
        _mapper.MapListToViewModel(books, booksModel);

        for (int i = 0; i < booksModel.Count; i++)
        {
            booksModel[i].HasBook = SetHasBook(userId, books[i]);
        }

        return booksModel;
    }

    public async Task HideAsync(string bookId)
    {
        var book = await _bookRepository.GetSingleByIdAsync(bookId);

        book!.IsHidden = true;
        await _bookRepository.SaveChangesAsync();
    }

    public async Task ShowAsync(string bookId)
    {
        var book = await _bookRepository.GetSingleByIdAsync(bookId);

        book!.IsHidden = false;
        await _bookRepository.SaveChangesAsync();
    }

    public async Task<bool> HasBookAsync(string bookId, string userId)
    {
        return await _bookRepository
                        .AnyAsync(
                            c => c.Id.ToString() == bookId && c.Readers.Any(r => r.Id.ToString() == userId));
    }

    public async Task RemoveAsync(string bookId, string userId)
    {
        var book = await _bookRepository.GetBookWithReaders(bookId);
        var user = await _userRepository.GetSingleByIdAsync(userId);

        book!.Readers.Remove(user!);
        await _userRepository.SaveChangesAsync();
    }

    public async Task<bool> IsHiddenAsync(string bookId)
    {
        return await _bookRepository.IsHiddenAsync(bookId);
    }

    private static bool SetHasBook(string userId, Book? book)
    {
        if (book!.Readers.Any(p => p.Id.ToString() == userId))
        {
            return true;
        }

        return false;
    }

}
