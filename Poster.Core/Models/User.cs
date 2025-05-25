using Microsoft.AspNetCore.Identity;

namespace Poster.Core.Models;

public class User : IdentityUser
{
    public required List<Post> Posts { get; set; }
}