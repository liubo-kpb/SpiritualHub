namespace SpiritualHub.Services.Mappings;

using AutoMapper;

using Client.ViewModels.Author;
using Client.ViewModels.Category;
using Data.Models;

public class ApplicationProfile : Profile
{
    public ApplicationProfile()
    {
        // source --> destination
        //Author
        CreateMap<Author, AuthorViewModel>().ReverseMap();
        CreateMap<Author, AuthorIndexViewModel>().ReverseMap();
        CreateMap<AuthorFormModel, Author>();

        //Category
        CreateMap<Category, CategoryServiceModel>().ReverseMap();
    }
}
