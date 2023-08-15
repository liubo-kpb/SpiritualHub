namespace SpiritualHub.WebAPI;

using Microsoft.EntityFrameworkCore;

using Data;
using Services.Interfaces;
using Services.Mappings;
using Client.Infrastructure.Extensions;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

        builder.Services.AddDbContext<SpiritsDbContext>(options =>
            options.UseSqlServer(connectionString));

        builder.Services.AddApplicationServices(typeof(IAuthorService));
        builder.Services.AddApplicationRepositories();
        builder.Services.AddAutoMapper(typeof(ApplicationProfile));
        builder.Services.AddControllers();
        
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddCors(setup =>
        {
            setup.AddPolicy("MainSpiritsDomain", policyBuilder =>
            {
                policyBuilder
                .WithOrigins("https://localhost:7279")
                .AllowAnyHeader()
                .AllowAnyMethod();
            });
        });

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.UseCors("MainSpiritsDomain");

        app.Run();
    }
}
