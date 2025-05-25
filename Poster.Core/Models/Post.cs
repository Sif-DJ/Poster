using System.ComponentModel.DataAnnotations;

namespace Poster.Core.Models;

public class Post
{
    [Key]
    public required int Id { get; set; }
    public required string Title { get; set; }
    public required string Content { get; set; }
    public required DateTime PublishDate { get; set; }
    public required User User { get; set; }
}