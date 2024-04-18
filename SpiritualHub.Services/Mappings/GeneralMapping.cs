namespace SpiritualHub.Services.Mappings;

using AutoMapper;

public static class GeneralMapping
{
    public static void MapListToViewModel<TEntity, TModel>(this IMapper mapper, IEnumerable<TEntity> entities, ICollection<TModel> allEntitiesModel)
    {
        foreach (var entity in entities)
        {
            TModel entityViewModel = mapper.Map<TModel>(entity);
            allEntitiesModel.Add(entityViewModel);
        }
    }
}
