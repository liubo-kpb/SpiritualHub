namespace SpiritualHub.Data;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using Configuration;
using Models;

public class SpiritsDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
{
    private readonly bool seedDb;

    public SpiritsDbContext(DbContextOptions<SpiritsDbContext> options, bool seedDb = true)
        : base(options)
    {
        this.seedDb = seedDb;
    }

    public DbSet<Author> Authors { get; set; }

    public DbSet<Blog> Blogs { get; set; }

    public DbSet<Publisher> Publishers { get; set; }

    public DbSet<Image> Images { get; set; }

    public DbSet<Subscription> Subscriptions { get; set; }

    public DbSet<SubscriptionType> SubscriptionTypes { get; set; }

    public DbSet<Book> Books { get; set; }

    public DbSet<Rating> Ratings { get; set; }

    public DbSet<Category> Categories { get; set; }

    public DbSet<Course> Courses { get; set; }

    public DbSet<Comment> Comments { get; set; }

    public DbSet<Event> Events { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new AuthorEntityConfiguration());
        builder.ApplyConfiguration(new BlogEntityConfiguration());
        builder.ApplyConfiguration(new BookEntityConfiguration());
        builder.ApplyConfiguration(new CommentEntityConfiguration());
        builder.ApplyConfiguration(new CourseEntityConfiguration());
        builder.ApplyConfiguration(new EventEntityConfiguration());
        builder.ApplyConfiguration(new RatingEntityConfiguration());
        builder.ApplyConfiguration(new SubscriptionEntityConfiguration());
        builder.ApplyConfiguration(new SubscriptionTypeEntityConfiguration());

        if (seedDb)
        {

        }

        base.OnModelCreating(builder);
    }
}
