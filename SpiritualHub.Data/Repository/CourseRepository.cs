namespace SpiritualHub.Data.Repository;

using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using Interface;
using Models;

public class CourseRepository : DeletableRepository<Course>, ICourseRepository
{
    public CourseRepository(SpiritsDbContext context) : base(context)
    {
    }

    public async Task<Course?> GetCourseDetailsAsync(string id) => await DbSet
                                                                            .Include(c => c.Image)
                                                                            .Include(c => c.Category)
                                                                            .Include(c => c.Author)
                                                                            .Include(c => c.Publisher)
                                                                            .ThenInclude(p => p.User)
                                                                            .Include(c => c.Modules)
                                                                            .FirstOrDefaultAsync(c => c.Id.ToString() == id);

    public async Task<Course?> GetCourseInfoAsync(string id) => await DbSet
                                                                        .Include(c => c.Image)
                                                                        .Include(c => c.Modules)
                                                                        .FirstOrDefaultAsync(c => c.Id.ToString() == id);

}
