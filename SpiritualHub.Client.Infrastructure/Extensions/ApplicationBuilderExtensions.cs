namespace SpiritualHub.Client.Infrastructure.Extensions;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

using Data.Models;
using Infrastructure.Middleware;

using static Common.GeneralApplicationConstants;

public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder SeedAdmin(this IApplicationBuilder app)
    {
        using IServiceScope scoredServices = app.ApplicationServices.CreateScope();

        var services = scoredServices.ServiceProvider;
        
        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole<Guid>>>();

        Task.Run(async () =>
        {
            if (await roleManager.RoleExistsAsync(AdminRoleName))
            {
                return;
            }

            var role = new IdentityRole<Guid> { Name = AdminRoleName };
            await roleManager.CreateAsync(role);

            var admin = await userManager.FindByEmailAsync(AdminEmail);

            await userManager.AddToRoleAsync(admin, role.Name);
        })
        .GetAwaiter()
        .GetResult();

        return app;
    }

    public static IApplicationBuilder EnableOnlineUsersCheck(this IApplicationBuilder app)
    {
        return app.UseMiddleware<OnlineUsersMiddleware>();
    }
}
