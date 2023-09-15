﻿namespace SpiritualHub.Services;

using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

using AutoMapper;
using Mappings;

using Data.Repository.Interface;
using Interfaces;
using Models.Book;
using Client.ViewModels.Book;
using Client.Infrastructure.Enums;
using AutoMapper.QueryableExtensions;

public class BookService : IBookService
{
    private readonly IBookRepository _bookRepository;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public BookService(
        IBookRepository bookRepository,
        IUserRepository userRepository,
        IMapper mapper)
    {
        _bookRepository = bookRepository;
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public Task AddAsync(string bookId, string userId)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<BookViewModel>> AllBooksByUserIdAsync(string userId)
    {
        var user = await _userRepository.GetUserWithBooks(userId);

        var booksModel = new List<BookViewModel>();
        _mapper.MapListToViewModel(user!.Books, booksModel);

        return booksModel;

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
                            .Include(b => b.Image)
                            .Include(b => b.Author)
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

    public async Task<IEnumerable<BookViewModel>> GetBooksByPublisherIdAsync(string publisherId)
    {
        return await _bookRepository
                            .AllAsNoTracking()
                            .Include(b => b.Image)
                            .Include(b => b.Author)
                            .Where(b => b.PublisherID.ToString() == publisherId)
                            .ProjectTo<BookViewModel>(_mapper.ConfigurationProvider)
                            .ToListAsync();
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
