using Poster.Core.Models;

namespace Poster.Core.DTOs;

public class PostDTO
{
    public required int Id { get; set; }
    public required string Title { get; set; }
    public required string Content { get; set; }
    public required DateTime PublishDate { get; set; }
    public required int UserId { get; set; }
}