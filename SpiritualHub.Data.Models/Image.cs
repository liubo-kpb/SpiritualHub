namespace SpiritualHub.Data.Models;

using System.ComponentModel.DataAnnotations;

public class Image
{
    public Image()
    {
        this.Id = Guid.NewGuid();
    }

    [Key]
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    [Required]
    public string URL { get; set; }
}
