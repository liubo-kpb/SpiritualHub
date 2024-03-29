namespace SpiritualHub.Services;

using Microsoft.EntityFrameworkCore;

using Data.Models;
using Data.Repository.Interfaces;
using Services.Interfaces;

public class BlogService : IBlogService
{

    private readonly IRepository<Blog> _blogRepository;

    public BlogService(IRepository<Blog> blogRepository)
    {
        _blogRepository = blogRepository;
    }

    public async Task<int> GetAllCountAsync()
    {
        return await _blogRepository
            .AllAsNoTracking()
            .CountAsync();
    }
}
