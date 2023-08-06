namespace SpiritualHub.Services.Mappings;

using AutoMapper;

using Client.ViewModels.Author;
using Data.Models;

public class ApplicationProfile : Profile
{
    public ApplicationProfile()
    {
        // source --> destination
        CreateMap<Author, AuthorViewModel>();
    }
}
