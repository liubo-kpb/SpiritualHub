namespace SpiritualHub.Services.Mappings;

using AutoMapper;

using Data.Models;
using Client.ViewModels.Author;
using Client.ViewModels.Category;
using Client.ViewModels.Event;
using Client.ViewModels.Publisher;
using Client.ViewModels.Subscription;
using Client.ViewModels.User;

public class ApplicationProfile : Profile
{
    public ApplicationProfile()
    {
        // source --> destination
        // Author
        CreateMap<Author, AuthorIndexViewModel>();
        CreateMap<Author, AuthorSubscribeFormModel>();
        CreateMap<Author, AuthorInfoViewModel>();
        CreateMap<Author, AuthorFormModel>().ReverseMap();
        SpecificAuthorMaps();

        // Event
        CreateMap<Event, EventViewModel>();
        CreateMap<Event, EventDetailsViewModel>();
        CreateMap<Event, EventFormModel>().ReverseMap();
        CreateMap<Event, EventInfoViewModel>();

        // Category
        CreateMap<Category, CategoryServiceModel>();

        // User
        CreateMap<ApplicationUser, UserServiceModel>()
            .ForMember(p => p.FullName, opt => opt.MapFrom(op => op.FirstName + " " + op.LastName))
            .ForMember(p => p.Email, opt => opt.MapFrom(op => op.Email));

        // Publisher
        CreateMap<Publisher, PublisherInfoViewModel>()
            .ForMember(p => p.FullName, opt => opt.MapFrom(op => op.User.FirstName + " " + op.User.LastName))
            .ForMember(p => p.Email, opt => opt.MapFrom(op => op.User.Email))
            .ForMember(p => p.PhoneNumber, opt => opt.MapFrom(op => op.PhoneNumber));

        CreateMap<Publisher, UserServiceModel>()
            .ForMember(p => p.Id, opt => opt.MapFrom(op => op.User.Id.ToString()))
            .ForMember(p => p.FullName, opt => opt.MapFrom(op => op.User.FirstName + " " + op.User.LastName))
            .ForMember(p => p.Email, opt => opt.MapFrom(op => op.User.Email))
            .ForMember(p => p.PhoneNumber, opt => opt.MapFrom(op => op.PhoneNumber));

        // Subscription
        CreateMap<Subscription, SubscriptionViewModel>()
            .ForMember(s => s.SubscriptionType,
            opt => opt.MapFrom(st => st.SubscriptionType.Type));
    }

    private void SpecificAuthorMaps()
    {
        //------------------------------------------------------------------------------------------------------------------------
        // Basically the same logic since Automapper doesn't seem to recognize the pattern on its own
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
        //------------------------------------------------------------------------------------------------------------------------
    }
}
