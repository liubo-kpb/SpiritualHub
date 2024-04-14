namespace SpiritualHub.Data.Repository;

using Models;
using System.Threading.Tasks;

public class CategoryRepository : DeletableRepository<Category>
{
    public CategoryRepository(SpiritsDbContext context) : base(context)
    {
    }

    public override async Task<Category?> GetSingleByIdAsync(string id)
    {
        return await DbSet.FindAsync(int.Parse(id));
    }
}
