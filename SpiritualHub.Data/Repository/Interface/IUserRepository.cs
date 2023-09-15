namespace SpiritualHub.Data.Repository.Interface;

using Models;

public interface IUserRepository : IRepository<ApplicationUser>
{
    Task<ApplicationUser?> GetUserWithBooks(string id);
}
