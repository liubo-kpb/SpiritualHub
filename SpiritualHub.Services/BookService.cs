namespace SpiritualHub.Services;

using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

using AutoMapper;
using Mappings;

using Data.Models;
using Data.Repository.Interface;
using Interfaces;
using Models.Book;
using Client.ViewModels.Book;
using Client.Infrastructure.Enums;

public class BookService : IBookService
{
    private readonly IBookRepository _bookRepository;
    private readonly IMapper _mapper;

    public BookService(
        IBookRepository bookRepository,
        IMapper mapper)
    {
        _bookRepository = bookRepository;
        _mapper = mapper;
    }

    public Task AddAsync(string bookId, string userId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<BookViewModel>> AllBooksByUserIdAsync(string userId)
    {
        throw new NotImplementedException();
    }

    public Task<string> CreateAsync(BookFormModel newBook)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(string bookId)
    {
        throw new NotImplementedException();
    }

    public Task EditAsync(BookFormModel updatedBook)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ExistsAsync(string id)
    {
        throw new NotImplementedException();
    }

    public async Task<FilteredBooksServiceModel> GetAllAsync(AllBooksQueryModel queryModel)
    {
        var booksQuery = _bookRepository
                            .AllAsNoTracking();

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
                            .Skip((queryModel.CurrentPage - 1) * queryModel.BooksPerPage)
                            .Take(queryModel.BooksPerPage)
                            .Include(e => e.Image)
                            .Include(e => e.Author)
                            .ToListAsync();

        List<BookViewModel> booksModel = new List<BookViewModel>();
        _mapper.MapListToViewModel(books, booksModel);

        return new FilteredBooksServiceModel()
        {
            Books = booksModel,
            TotalBooksCount = books.Count
        };
    }

    public async Task<int> GetAllCountAsync()
    {
        return await _bookRepository
            .AllAsNoTracking()
            .CountAsync();
    }

    public Task<string> GetAuthorIdAsync(string bookId)
    {
        throw new NotImplementedException();
    }

    public async Task<BookDetailsViewModel> GetBookDetailsAsync(string id)
    {
        var bookEntity = await _bookRepository.GetFullBookDetails(id);
        var bookModel = _mapper.Map<BookDetailsViewModel>(bookEntity);

        return bookModel;
    }

    public Task<BookFormModel> GetBookInfoAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<BookViewModel>> GetBooksByPublisherIdAsync(string publisherId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> IsAddedAsync(string bookId, string userId)
    {
        throw new NotImplementedException();
    }

    public Task RemoveAsync(string bookId, string userId)
    {
        throw new NotImplementedException();
    }
}
