namespace SpiritualHub.Client;

using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

using Infrastructure.Extensions;
using Infrastructure.ModelBinder;
using Data;
using Data.Models;
using Services.Interfaces;
using Services.Mappings;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

        builder.Services.AddDbContext<SpiritsDbContext>(options =>
            options.UseSqlServer(connectionString));

        builder.Services.AddDatabaseDeveloperPageExceptionFilter();

        builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
        {
            options.SignIn.RequireConfirmedAccount = builder.Configuration.GetValue<bool>("Identity:SignIn:RequireConfirmedAccount");
            options.Password.RequireLowercase = builder.Configuration.GetValue<bool>("Identity:Password:RequireLowercase");
            options.Password.RequireUppercase = builder.Configuration.GetValue<bool>("Identity:Password:RequireUppercase");
            options.Password.RequireNonAlphanumeric = builder.Configuration.GetValue<bool>("Identity:Password:RequireAlphanumeric");
            options.Password.RequiredLength = builder.Configuration.GetValue<int>("Identity:Password:RequireLength");
        })
            .AddEntityFrameworkStores<SpiritsDbContext>();

        builder.Services.AddApplicationServices(typeof(IAuthorService));
        builder.Services.AddApplicationRepositories();
        builder.Services.AddAutoMapper(typeof(ApplicationProfile));
        builder.Services.AddControllersWithViews(options =>
        {
            options.ModelBinderProviders.Insert(0, new DecimalModelBinderProvider());
            options.Filters.Add<AutoValidateAntiforgeryTokenAttribute>();
        });

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseMigrationsEndPoint();
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error/500");
            app.UseStatusCodePagesWithRedirects("/Home/Error?statusCode={0}");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();


        app.UseEndpoints(endpoints =>
        {
            //endpoints.MapControllerRoute(
            //    name: "ProtectedUrlPattern",
            //    pattern: "/{controller}/{action}/{id}/{information}",
            //    defaults: new {Controller = "Category", Action = "Details"});

            endpoints.MapDefaultControllerRoute();
            endpoints.MapRazorPages();
        });

        app.Run();
    }
}