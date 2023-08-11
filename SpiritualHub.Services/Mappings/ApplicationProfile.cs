namespace SpiritualHub.Services.Mappings;

using AutoMapper;

using Data.Models;
using Client.ViewModels.Author;
using Client.ViewModels.Category;
using Client.ViewModels.Publisher;

public class ApplicationProfile : Profile
{
    public ApplicationProfile()
    {
        // source --> destination
        //Author
        CreateMap<Author, AuthorIndexViewModel>();
        CreateMap<Author, AuthorSubscribeFormModel>();
        CreateMap<Author, AuthorFormModel>().ReverseMap();
        SpecificAuthorMaps();

        //Category
        CreateMap<Category, CategoryServiceModel>().ReverseMap();

        //Publisher
        CreateMap<Publisher, PublisherInfoViewModel>()
            .ForMember(p => p.Email, opt => opt.MapFrom(op => op.User.Email))
            .ForMember(p => p.PhoneNumber, opt => opt.MapFrom(op => op.PhoneNumber));
    }

    private void SpecificAuthorMaps()
    {
        CreateMap<Author, AuthorDetailsViewModel>()
            .ForMember(a => a.SubscriberCount,
                       opt => opt.MapFrom(
                           au => au.Subscriptions.Sum(
                               s => s.Subscribers.Count)))
            .ForMember(a => a.FollowerCount,
                       opt => opt.MapFrom(f => f.Followers.Count));

        CreateMap<Author, AuthorViewModel>()
            .ForMember(a => a.SubscriberCount,
                       opt => opt.MapFrom(
                           au => au.Subscriptions.Sum(
                               s => s.Subscribers.Count)))
            .ForMember(a => a.FollowerCount,
                       opt => opt.MapFrom(f => f.Followers.Count));
    }
}
