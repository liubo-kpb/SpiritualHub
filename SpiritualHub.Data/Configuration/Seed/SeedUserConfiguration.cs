namespace SpiritualHub.Data.Configuration.Seed;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Configuration.Seed.Interface;
using Models;

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
        ApplicationUser user;

        user = new ApplicationUser()
        {
            Id = Guid.Parse("194974cd-73f0-4946-ba85-710d4061472d"),
            UserName = "publisher",
            NormalizedUserName = "PUBLISHER",
            Email = "publisher@spirits.com",
            NormalizedEmail = "PUBLISHER@SPIRITS.COM"
        };
        user.PasswordHash = hasher.HashPassword(user, "publish123");
        users.Add(user);

        user = new ApplicationUser()
        {
            Id = Guid.Parse("1fd95f69-4f9d-4671-b126-cefcf4b8a95e"),
            UserName = "user",
            NormalizedUserName = "USER",
            Email = "user@mail.com",
            NormalizedEmail = "USER@MAIL.COM"
        };
        user.PasswordHash = hasher.HashPassword(user, "using123");
        users.Add(user);

        return users.ToArray();
    }
}
