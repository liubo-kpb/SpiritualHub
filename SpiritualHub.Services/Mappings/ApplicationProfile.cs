namespace SpiritualHub.Services.Mappings;

using AutoMapper;
using SpiritualHub.Client.ViewModels.Author;
using SpiritualHub.Data.Models;

public class ApplicationProfile : Profile
{
    public ApplicationProfile()
    {
        // source --> destination
        CreateMap<Author, AllAuthorsQueryModel>();
    }
}
