namespace SpiritualHub.Data;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using Models;
using Configuration;
using Configuration.Seed;

public class SpiritsDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
{
    private readonly bool seedDb;

    public SpiritsDbContext(DbContextOptions<SpiritsDbContext> options, bool seedDb = true)
        : base(options)
    {
        this.seedDb = seedDb;
    }

    public DbSet<Author> Authors { get; set; } = null!;

    public DbSet<Blog> Blogs { get; set; } = null!;

    public DbSet<Book> Books { get; set; } = null!;

    public DbSet<Category> Categories { get; set; } = null!;

    public DbSet<Comment> Comments { get; set; } = null!;

    public DbSet<Course> Courses { get; set; } = null!;

    public DbSet<Event> Events { get; set; } = null!;

    public DbSet<Image> Images { get; set; } = null!;

    public DbSet<Publisher> Publishers { get; set; } = null!;

    public DbSet<Rating> Ratings { get; set; } = null!;

    public DbSet<Subscription> Subscriptions { get; set; } = null!;

    public DbSet<SubscriptionType> SubscriptionTypes { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new AuthorEntityConfiguration());
        builder.ApplyConfiguration(new BlogEntityConfiguration());
        builder.ApplyConfiguration(new BlogPostImageConfiguration());
        builder.ApplyConfiguration(new BookEntityConfiguration());
        builder.ApplyConfiguration(new CommentEntityConfiguration());
        builder.ApplyConfiguration(new CourseEntityConfiguration());
        builder.ApplyConfiguration(new EventEntityConfiguration());
        builder.ApplyConfiguration(new RatingEntityConfiguration());
        builder.ApplyConfiguration(new SubscriptionEntityConfiguration());
        builder.ApplyConfiguration(new SubscriptionTypeEntityConfiguration());

        if (this.seedDb)
        {
            builder.ApplyConfiguration(new SeedCategoryConfiguration());
            builder.ApplyConfiguration(new SeedSubscriptionTypeConfiguration());
            builder.ApplyConfiguration(new SeedImageConfiguration());
            builder.ApplyConfiguration(new SeedAuthorConfiguration());
            builder.ApplyConfiguration(new SeedEventConfiguration());
            builder.ApplyConfiguration(new SeedBookConfiguration());
            builder.ApplyConfiguration(new SeedUserConfiguration());
            builder.ApplyConfiguration(new SeedPublisherConfiguration());
            builder.ApplyConfiguration(new SeedSubscriptionConfiguration());
        }

        base.OnModelCreating(builder);
    }
}
