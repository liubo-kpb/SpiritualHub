namespace SpiritualHub.Data.Models;

using System.ComponentModel.DataAnnotations;

using static SpiritualHub.Common.EntityValidationConstants.Comment;

public class Comment
{
    public Comment()
    {
        this.Id = Guid.NewGuid();
    }

    [Key]
    public Guid Id { get; set; }

    [Required]
    [StringLength(TextMaxLength, MinimumLength = TextMinLength)]
    public string Text { get; set; } = null!;

    [Required]
    public Guid PostID { get; set; }

    public virtual Blog Post { get; set; } = null!;

    [Required]
    public Guid UserID { get; set; }

    public virtual ApplicationUser User { get; set; } = null!;
}
