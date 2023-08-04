﻿namespace SpiritualHub.Data.Models;

using System.ComponentModel.DataAnnotations;

using static Common.EntityValidationConstants.Publisher;

public class Publisher
{
    public Publisher()
    {
        this.Id = Guid.NewGuid();
        this.PublishedAuthors = new HashSet<Author>();
    }

    [Key]
    public Guid Id { get; set; }

    [Required]
    [MaxLength(PhoneNumberMaxLength)]
    public string PhoneNumber { get; set; } = null!;

    public Guid UserID { get; set; }

    public virtual ApplicationUser User { get; set; }

    public virtual ICollection<Author> PublishedAuthors { get; set; }

    public virtual ICollection<Event> Events { get; set; }

    public virtual ICollection<Book> Books { get; set; }

    public virtual ICollection<Course> Courses { get; set; }

    public virtual ICollection<Blog> Blogs { get; set; }
}
