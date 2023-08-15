namespace SpiritualHub.Services;

using Microsoft.EntityFrameworkCore;

using Data.Models;
using Data.Repository.Interface;
using Services.Interfaces;

public class BookService : IBookService
{
    private readonly IRepository<Book> _bookRepository;

    public BookService(IRepository<Book> bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task<int> GetAllCountAsync()
    {
        return await _bookRepository
            .AllAsNoTracking()
            .CountAsync();
    }
}
