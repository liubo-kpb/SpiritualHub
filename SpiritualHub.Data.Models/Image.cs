namespace SpiritualHub.Data.Models;

using System.ComponentModel.DataAnnotations;

public class Image
{
    public Image()
    {
        Id = Guid.NewGuid();
    }

    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    [Required]
    public string URL { get; set; }
}
