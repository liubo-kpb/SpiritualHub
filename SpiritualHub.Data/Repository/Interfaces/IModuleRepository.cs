namespace SpiritualHub.Data.Repository.Interfaces;

using Data.Models;

public interface IModuleRepository : IDeletableRepository<Module>
{
    Task<string?>       GetCourseIdByModuleId(string id);

    Task<string?>       GetAuthordId(string id);

    IQueryable<Module>  GetModulesByCourseId(string courseId);

}
