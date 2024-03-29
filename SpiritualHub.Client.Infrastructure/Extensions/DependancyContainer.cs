namespace SpiritualHub.Client.Infrastructure.Extensions;

using System.Reflection;

using Microsoft.Extensions.DependencyInjection;

using Data.Models;
using Data.Repository;
using Data.Repository.Interfaces;

public static class DependancyContainer
{
    /// <summary>
    /// This method registers all services with their interfaces and implementations of given assembly.
    /// The assembly is taken from the type of random service interface or implementation provided.
    /// </summary>
    /// <param name="serviceType"></param>
    /// <exception cref="InvalidOperationException"></exception>
    public static void AddApplicationServices(this IServiceCollection services, Type serviceType)
    {
        Assembly? serviceAssembly = Assembly.GetAssembly(serviceType);
        if (serviceAssembly == null)
        {
            throw new InvalidOperationException("Invalid service type provided!");
        }

        Type[] implementationTypes = serviceAssembly
            .GetTypes()
            .Where(t => t.Name.EndsWith("Service") && !t.IsInterface)
            .ToArray();

        foreach (Type implementationType in implementationTypes)
        {
            Type? interfaceType = implementationType
                .GetInterface($"I{implementationType.Name}");
            if (interfaceType == null)
            {
                throw new InvalidOperationException(
                    $"No interface is provided for the service with name: {implementationType.Name}");
            }

            services.AddScoped(interfaceType, implementationType);
        }
    }

    /// <summary>
    /// This method registers all repositories with their interfaces in the given assembly. Basic CRUD operations are
    /// located in a generic repository pattern. Manual addition of new repositories is mandatory for this method.
    /// </summary>
    /// <param name="services"></param>
    public static void AddApplicationRepositories(this IServiceCollection services)
    {
        services.AddScoped<IAuthorRepository, AuthorRepository>();
        services.AddScoped<IPublisherRepository, PublisherRepository>();
        services.AddScoped<IRepository<Subscription>, Repository<Subscription>>();
        services.AddScoped<IDeletableRepository<Category>, DeletableRepository<Category>>();
        services.AddScoped<IDeletableRepository<Image>, DeletableRepository<Image>>();
        services.AddScoped<IDeletableRepository<Rating>, DeletableRepository<Rating>>();
        services.AddScoped<IRepository<ApplicationUser>, Repository<ApplicationUser>>();
        services.AddScoped<IEventRepository, EventRepository>();
        services.AddScoped<ICourseRepository, CourseRepository>();
        services.AddScoped<IModuleRepository, ModuleRepository>();
        services.AddScoped<IBookRepository, BookRepository>();
        services.AddScoped<IRepository<Blog>, Repository<Blog>>();
    }
}
