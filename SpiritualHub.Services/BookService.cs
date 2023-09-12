namespace SpiritualHub.Services;

using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

using Data.Models;
using Data.Repository.Interface;
using Interfaces;
using Models.Event;
using Client.ViewModels.Book;

public class BookService : IBookService
{
    private readonly IRepository<Book> _bookRepository;

    public BookService(IRepository<Book> bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public Task AddAsync(string bookId, string userId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<BookViewModel>> AllBooksByUserIdAsync(string userId)
    {
        throw new NotImplementedException();
    }

    public Task<string> CreateAsync(BookFormModel newEvent)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(string bookId)
    {
        throw new NotImplementedException();
    }

    public Task EditAsync(BookFormModel updatedEvent)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ExistsAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<FilteredEventsServiceModel> GetAllAsync(AllBooksQueryModel queryModel, string userId)
    {
        throw new NotImplementedException();
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

    public Task<BookDetailsViewModel> GetBookDetailsAsync(string id, string userId)
    {
        throw new NotImplementedException();
    }

    public Task<BookFormModel> GetEventInfoAsync(string id)
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
