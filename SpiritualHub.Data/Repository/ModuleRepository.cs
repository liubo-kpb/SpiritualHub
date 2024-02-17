namespace SpiritualHub.Data.Repository;

using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using Interface;
using Models;
using System.Linq;

public class ModuleRepository : DeletableRepository<Module>, IModuleRepository
{
    public ModuleRepository(SpiritsDbContext context) : base(context)
    {
    }

    public async Task<string?> GetAuthordId(string id) => await DbSet
                                                                    .Include(m => m.Course)
                                                                    .Where(m => m.Id.ToString() == id)
                                                                    .Select(m => m.Course.AuthorID.ToString())
                                                                    .FirstOrDefaultAsync();

    public async Task<string?> GetCourseIdByModuleId(string id) => await DbSet
                                                                            .Where(m => m.Id.ToString() == id)
                                                                            .Select(m => m.CourseID.ToString())
                                                                            .FirstOrDefaultAsync();

    public IQueryable<Module> GetModulesByCourseId(string courseId) => DbSet
                                                                        .Where(m => m.CourseID.ToString() == courseId);
}
