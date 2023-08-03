namespace SpiritualHub.Data.Configuration.Seed;

using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Configuration.Seed.Interface;
using Models;

public class SeedBookConfiguration : IEntitySeedConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.HasData(GenerateEntities());
    }

    public Book[] GenerateEntities()
    {
        ICollection<Book> books = new HashSet<Book>();

        Book book;

        book = new Book()
        {
            Id = Guid.Parse("e0aa1a89-c180-4ac0-935d-8efab304b274"),
            Title = "Encyclopedia Cogitality",
            Description = "All the facts, in the beginning, were puzzle pieces, scattered in vastness - fragmented, incongruous, unordered. They arrived haphazardly in moments when you weren't seeking them and not expecting them... Flashes, illuminating the darkness, which it is fitting to capture in your hands like fireflies - to gather them with patience, inspiration, and dedication. Then, embracing the scattered chaos of your own ignorance, with faith in the Nothingness, you arrange the light of your own Life.",
            ShortDescription = "Bundle of the books You - The Source, You - The Manifestation, You - The Life",
            Price = 30,
            CategoryID = 3,
            AuthorID = Guid.Parse("240ae09a-7f04-45e5-ac42-bf5311e1c4a8"),
            ImageID = Guid.Parse("c7b99bd1-8188-4277-b937-81ab367b4034"),
        };
        books.Add(book);

        book = new Book()
        {
            Id = Guid.Parse("12221379-d5c7-4688-8ad8-efbffcaf8d06"),
            Title = "The Masters of Limitation",
            Description = "All the facts, in the beginning, were puzzle pieces, scattered in vastness - fragmented, incongruous, unordered. They arrived haphazardly in moments when you weren't seeking them and not expecting them... Flashes, illuminating the darkness, which it is fitting to capture in your hands like fireflies - to gather them with patience, inspiration, and dedication. Then, embracing the scattered chaos of your own ignorance, with faith in the Nothingness, you arrange the light of your own Life.",
            ShortDescription = "Bundle of the books You - The Source, You - The Manifestation, You - The Life",
            Price = 30,
            CategoryID = 2,
            AuthorID = Guid.Parse("47383fe7-f3e1-4d22-8180-5bfaa76955f5"),
            ImageID = Guid.Parse("c7b99bd1-8188-4277-b937-81ab367b4034"),
        };
        books.Add(book);

        book = new Book()
        {
            Id = Guid.Parse("641ae624-efd0-4eb6-87af-05f2cc17bbb7"),
            Title = "The Power Of Now",
            Description = $"It's no wonder that The Power of Now has sold over 2 million copies worldwide and has been translated into over 30 foreign languages. Much more than simple principles and platitudes, the book takes readers on an inspiring spiritual journey to find their true and deepest self and reach the ultimate in personal growth and spirituality: the discovery of truth and light.\r{Environment.NewLine}\r{Environment.NewLine}In the first chapter, Tolle introduces readers to enlightenment and its natural enemy, the mind. He awakens readers to their role as a creator of pain and shows them how to have a pain-free identity by living fully in the present. The journey is thrilling, and along the way, the author shows how to connect to the indestructible essence of our Being, \"the eternal, ever-present One Life beyond the myriad forms of life that are subject to birth and death.\"\r{Environment.NewLine}\r{Environment.NewLine}Featuring a new preface by the author, this paperback shows that only after regaining awareness of Being, liberated from Mind and intensely in the Now, is there Enlightenment.",
            ShortDescription = "This book shows that only after regaining awareness of Being, liberated from Mind and intensely in the Now, is there Enlightenment",
            Price = 30,
            CategoryID = 5,
            AuthorID = Guid.Parse("8c8bd426-2974-4bad-aa33-0e045ca86a54"),
            ImageID = Guid.Parse("327b0419-5ff9-4694-a4f8-151cb0a46e6b"),
        };
        books.Add(book);

        return books.ToArray();
    }
}
