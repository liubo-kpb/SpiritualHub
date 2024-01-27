namespace SpiritualHub.Client.Controllers;

using System.Collections.Generic;
using System.Threading.Tasks;

using Client.ViewModels.BaseModels;
using Client.ViewModels.Module;
using Services.Interfaces;
using Data.Models;

public class ModuleController : BaseController<EmptyViewModel, ModuleDetailsViewModule, ModuleFormModel, EmptyQueryModel, Enum>
{
    private readonly IModuleService _moduleService;

    public ModuleController(
        IModuleService moduleService,
        IAuthorService authorService,
        ICategoryService categoryService,
        IPublisherService publisherService)
        : base(authorService, categoryService, publisherService, nameof(Module).ToLower())
    {
        _moduleService = moduleService;
    }

    protected override Task<string> CreateAsync(ModuleFormModel newEntity)
    {
        throw new NotImplementedException();
    }

    protected override Task EditAsync(ModuleFormModel updatedEntityFrom)
    {
        throw new NotImplementedException();
    }

    protected override Task<bool> ExistsAsync(string id)
    {
        throw new NotImplementedException();
    }

    protected override Task<EmptyQueryModel> GetAllAsync(EmptyQueryModel queryModel, string userId)
    {
        throw new NotImplementedException();
    }

    protected override Task<IEnumerable<EmptyViewModel>> GetAllEntitiesByUserId(string userId)
    {
        throw new NotImplementedException();
    }

    protected override Task<IEnumerable<EmptyViewModel>> GetEntitiesByPublisherIdAsync(string publisherId, string userId)
    {
        throw new NotImplementedException();
    }

    protected override Task<ModuleDetailsViewModule> GetEntityDetails(string id, string userId)
    {
        throw new NotImplementedException();
    }

    protected override Task<ModuleFormModel> GetEntityInfoAsync(string id)
    {
        throw new NotImplementedException();
    }
}
