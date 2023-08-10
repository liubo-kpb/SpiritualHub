namespace SpiritualHub.Common;

public static class EntityValidationConstants
{

    public static class Author
    {
        public const int AliasMaxLength = 50;
        public const int AliasMinLength = 3;

        public const int NameMaxLength = 50;
        public const int NameMinLength = 5;
    }

    public static class Blog
    {
        public const int TitleMaxLength = 50;
        public const int TitleMinLength = 5;

        public const int ShortDesciptionMaxLength = 150;
        public const int ShortDescriptionMinLength = 30;
    }

    public static class Book
    {
        public const int TitleMaxLength = 30;
        public const int TitleMinLength = 5;
    }

    public static class Course
    {
        public const int NameMaxLength = 30;
        public const int NameMinLength = 5;

        public const int DescriptionMinLength = 30;

        public const int ShortDescriptionMaxLength = 50;
        public const int ShortDescriptionMinLength = 15;
    }

    public static class Comment
    {
        public const int TextMaxLength = 300;
        public const int TextMinLength = 10;
    }

    public static class Event
    {
        public const int TitleMaxLength = 50;
        public const int TitleMinLength = 5;

        public const int DescriptionMinLength = 30;
    }

    public static class Publisher
    {
        public const int PhoneNumberMaxLength = 15;
        public const int PhoneNumberMinLength = 7;
    }
}
