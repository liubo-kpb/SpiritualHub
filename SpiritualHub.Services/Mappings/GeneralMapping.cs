namespace SpiritualHub.Services.Mappings;

using AutoMapper;

internal class GeneralMapping
{
    public static void MapListToViewModel<TEntity, TModel>(IMapper mapper, IEnumerable<TEntity> entities, ICollection<TModel> allEntitiesModel)
    {
        foreach (var entity in entities)
        {
            TModel entityViewModel = mapper.Map<TModel>(entity);
            allEntitiesModel.Add(entityViewModel);
        }
    }
}
