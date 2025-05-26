using System.ComponentModel.DataAnnotations;

namespace Poster.Core.Models;

public class User
{
    [Key]
    public required int Id { get; set; }
    public required string UserName { get; set; }
    public required string Password { get; set; }
    public List<Post> Posts { get; set; } = new List<Post>();
}