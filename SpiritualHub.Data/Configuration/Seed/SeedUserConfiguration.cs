namespace SpiritualHub.Data.Configuration.Seed;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Configuration.Seed.Interface;
using Models;

using static Common.GeneralApplicationConstants;

public class SeedUserConfiguration : IEntitySeedConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.HasData(GenerateEntities());
    }

    public ApplicationUser[] GenerateEntities()
    {
        ICollection<ApplicationUser> users = new HashSet<ApplicationUser>();

        var hasher = new PasswordHasher<ApplicationUser>();
        ApplicationUser admin = new ApplicationUser()
        {
            Id = Guid.Parse("bcb4f072-ecca-43c9-ab26-c060c6f364e4"),
            Email = AdminEmail,
            NormalizedEmail = AdminEmail.ToUpper(),
            UserName = AdminEmail,
            NormalizedUserName = AdminEmail.ToUpper(),
            FirstName = "Great",
            LastName = "Admin",
            SecurityStamp = Guid.NewGuid().ToString(),
        };

        admin.PasswordHash = hasher.HashPassword(admin, "admin123");
        users.Add(admin);

        ApplicationUser user;

        user = new ApplicationUser()
        {
            Id = Guid.Parse("194974cd-73f0-4946-ba85-710d4061472d"),
            FirstName = "Pablo",
            LastName = "Publish",
            UserName = "publisher@spirits.com",
            NormalizedUserName = "PUBLISHER@SPIRITS.COM",
            Email = "publisher@spirits.com",
            NormalizedEmail = "PUBLISHER@SPIRITS.COM",
            SecurityStamp = Guid.NewGuid().ToString(),
        };
        user.PasswordHash = hasher.HashPassword(user, "publish123");
        users.Add(user);

        user = new ApplicationUser()
        {
            Id = Guid.Parse("1fd95f69-4f9d-4671-b126-cefcf4b8a95e"),
            FirstName = "Martin",
            LastName = "User",
            UserName = "user@mail.com",
            NormalizedUserName = "USER@MAIL.COM",
            Email = "user@mail.com",
            NormalizedEmail = "USER@MAIL.COM",
            SecurityStamp = Guid.NewGuid().ToString(),
        };
        user.PasswordHash = hasher.HashPassword(user, "using123");
        users.Add(user);

        user = new ApplicationUser()
        {
            Id = Guid.Parse("187E0540-5A90-419A-BF5B-F65EE213A0CA"),
            UserName = "noname@mail.com",
            NormalizedUserName = "NONAME@MAIL.COM",
            Email = "noname@mail.com",
            NormalizedEmail = "NONAME@MAIL.COM",
            SecurityStamp = Guid.NewGuid().ToString(),
        };
        user.PasswordHash = hasher.HashPassword(user, "noname123");
        users.Add(user);

        return users.ToArray();
    }
}
