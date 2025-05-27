using Poster.Core.Models;

namespace Poster.Core.DTOs;

public class UserDTO
{
    public required string Username { get; set; }
    public required List<PostDTO> Posts { get; set; }
}