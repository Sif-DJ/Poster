namespace Poster.Core.Models;

public class Post
{
    public required string Title { get; set; }
    public required string Content { get; set; }
    public required DateTime PublishDate { get; set; }
    public required User User { get; set; }
}