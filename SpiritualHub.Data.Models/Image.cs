﻿namespace SpiritualHub.Data.Models;

using System.ComponentModel.DataAnnotations;

public class Image
{
    public Image()
    {
        this.Id = Guid.NewGuid();
    }

    [Key]
    public Guid Id { get; set; }

    public string Name { get; set; }

    [Required]
    public string URL { get; set; }

    public Guid CourseID { get; set; }

    public virtual Course Course { get; set; }

    public Guid BlogID { get; set; }

    public virtual Blog Blog { get; set; }
}
