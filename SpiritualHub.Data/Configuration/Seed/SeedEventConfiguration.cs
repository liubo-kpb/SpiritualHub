namespace SpiritualHub.Data.Configuration.Seed;

using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Configuration.Seed.Interface;
using Models;

public class SeedEventConfiguration : IEntitySeedConfiguration<Event>
{
    public void Configure(EntityTypeBuilder<Event> builder)
    {
        builder.HasData(GenerateEntities());
    }

    public Event[] GenerateEntities()
    {
        ICollection<Event> events = new HashSet<Event>();

        Event e;

        e = new Event()
        {
            Id = Guid.Parse("15f326bc-f47f-487f-8764-5fb8fd5c448a"),
            Title = "The 3 Behaviors of Connection",
            Description = $"What if there was one state of being we could adopt that would help us establish better, stronger connections not only with our families and friends on earth, but also with our friends from the stars?\r{Environment.NewLine}\r{Environment.NewLine}In The Three Behaviors of Connection, Bashar will share how action, timing, and communication are vital concepts for making inroads and connection with the hybrid children that will eventually be living among us. He will expand in detail on these three behaviors and how we might apply them to our lives on Earth as well as to our quest for contact with our extraterrestrial family.",
            Price = 35,
            CreatedOn = DateTime.UtcNow,
            StartDateTime = new DateTime(2023, 08, 26, 14, 30, 00),
            EndDateTime = new DateTime(2023, 08, 26, 15, 30, 00),
            IsOnline = true,
            CategoryID = 2,
            AutorID = Guid.Parse("47383fe7-f3e1-4d22-8180-5bfaa76955f5"),
            ImageID = Guid.Parse("69630e42-a4de-4116-a1a4-38c43faa0b53"),
        };
        events.Add(e);

        e = new Event()
        {
            Id = Guid.Parse("45bb1c09-b50d-4d47-8fdb-fbfb53086922"),
            Title = "An Evening with Eckhart Tolle in Stockholm",
            Description = $"Join us for this unique opportunity to sit with Eckhart Tolle as he points you to spiritual awakening and the transformation of consciousness. With his hallmark warmth, humour and compassion, this evening will connect you with the peace and serenity that arises from living in the moment.\r{Environment.NewLine}\r{Environment.NewLine}Eckhart’s profound, yet simple teachings have helped countless people from around the globe awaken to a vibrantly alive inner peace in their daily lives. Eckhart Tolle’s writings and life-changing public events have touched millions of lives, garnering fans to the likes of Oprah, the Dalai Lama and Deepak Chopra. He is the best-selling author of The Power of Now and A New Earth that are widely regarded as the most transformational books of our time.",
            Price = 199,
            CreatedOn = DateTime.UtcNow,
            StartDateTime = new DateTime(2023, 09, 26, 18, 30, 00),
            EndDateTime = new DateTime(2023, 09, 26, 22, 00, 00),
            IsOnline = false,
            CategoryID = 5,
            AutorID = Guid.Parse("8c8bd426-2974-4bad-aa33-0e045ca86a54"),
            ImageID = Guid.Parse("13e26f61-5a34-44e0-b9d4-d8ab04b8f342"),
        };
        events.Add(e);

        e = new Event()
        {
            Id = Guid.Parse("3db097df-7c7c-4c4e-b546-d4555c4c1521"),
            Title = "Seminar - Campus \"Healing\"",
            Description = $"The Cogitality seminars are back - they have already started in the country, and now they are happening at the \"Healing\" campus too! They are pre-planned and organized by the team of cogitalists.\r{Environment.NewLine}\r{Environment.NewLine}The first seminar at the \"Healing\" campus, which will take place on September 2-3, 2023, is already fully booked. Thank you for the sincere desire to share this experience together!",
            Price = 144,
            CreatedOn = DateTime.UtcNow,
            StartDateTime = new DateTime(2023, 09, 02, 09, 00, 00),
            EndDateTime = new DateTime(2023, 09, 03, 18, 00, 00),
            IsOnline = true,
            CategoryID = 3,
            AutorID = Guid.Parse("240ae09a-7f04-45e5-ac42-bf5311e1c4a8"),
            ImageID = Guid.Parse("ab7cfc34-55f4-4ed8-9687-c48a747e9fb4"),
        };
        events.Add(e);

        return events.ToArray();
    }
}
