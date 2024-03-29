﻿namespace SpiritualHub.Common;

public static class EntityValidationConstants
{
    public static class ApplicationUser
    {
        public const int FirstNameMaxLength = 12;
        public const int FirstNameMinLength = 1;

        public const int LastNameMaxLength = 15;
        public const int LastNameMinLength = 3;
    }

    public static class Author
    {
        public const int AliasMaxLength = 50;
        public const int AliasMinLength = 3;

        public const int NameMaxLength = 50;
        public const int NameMinLength = 5;
    }

    public static class Blog
    {
        public const int TitleMaxLength = 150;
        public const int TitleMinLength = 5;

        public const int ShortDesciptionMaxLength = 250;
        public const int ShortDescriptionMinLength = 30;
    }

    public static class Book
    {
        public const int TitleMaxLength = 30;
        public const int TitleMinLength = 5;

        public const int ShortDescriptionMaxLength = 250;
        public const int ShortDescriptionMinLength = 15;

        public const int DescriptionMinLength = 50;
    }

    public static class Course
    {
        public const int NameMaxLength = 80;
        public const int NameMinLength = 5;

        public const int DescriptionMinLength = 30;

        public const int ShortDescriptionMaxLength = 250;
        public const int ShortDescriptionMinLength = 15;
    }

    public static class Module
    {
        public const int NameMaxLength = 80;
        public const int NameMinLength = 5;

        public const int DescriptionMaxLength = 250;
        public const int DescriptionMinLength = 15;
    }

    public static class Comment
    {
        public const int TextMaxLength = 600;
        public const int TextMinLength = 10;
    }

    public static class Event
    {
        public const int TitleMaxLength = 80;
        public const int TitleMinLength = 5;

        public const int DescriptionMinLength = 30;
    }

    public static class Publisher
    {
        public const int PhoneNumberMaxLength = 15;
        public const int PhoneNumberMinLength = 7;
    }

    public static class Rating
    {
        public const int MaxStar = 10;
        public const int MinStar = 0;

        public const int HeadlineMaxLength = 200;

        public const int TextMaxLength = 600;
    }
}
