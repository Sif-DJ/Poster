using System.Security.Cryptography;
using System.Web.Helpers;
using Microsoft.EntityFrameworkCore;
using Poster.Core.DTOs;
using Poster.Core.Models;
using static Poster.Core.Extensions;

namespace Poster.Infrastructure.Repositories;

public class UserRepository(PosterContext context)
{
    public async Task<int> GetAmountOfUsers()
    {
        var list = await context.Users.ToListAsync();
        return list.Count();
    }

    public async Task<bool> UserNameExists(string userName)
    {
        return await context.Users.AnyAsync(x => x.UserName.Equals(userName));
    }

    public async Task AddNewUser(string userName, string password)
    {
        await context.Users.AddAsync(new User
        {
            Id = 0,
            UserName = userName,
            Password = Crypto.HashPassword(password)
        });
        await context.SaveChangesAsync();
    }

    public async Task<UserDTO?> Login(string username, string password)
    {
        var user = await context.Users.FirstOrDefaultAsync(x => x.UserName.Equals(username));
        if (user == null)
            return null;
        return Crypto.VerifyHashedPassword(user.Password, password) ? GetUserDTO(user) : null;
    }

    public async Task<UserDTO?> GetUserDTOByName(string userName)
    {
        var user = await context.Users
            .Include(user => user.Posts)
            .FirstOrDefaultAsync(x => x.UserName.Equals(userName));
        if (user == null)
        {
            Console.WriteLine("No user found");
            return null;
        }
        var dto = GetUserDTO(user);
        return dto;
    }

    public async Task<UserDTO?> GetUserDTOById(int userId)
    {
        var user = await context.Users
            .Include(user => user.Posts)
            .FirstOrDefaultAsync(x => x.Id.Equals(userId));
        if (user == null)
        {
            Console.WriteLine("No user found");
            return null;
        }
        var dto = GetUserDTO(user);
        return dto;
    }

    private UserDTO GetUserDTO(User user)
    {
        return new UserDTO
        {
            Username = user.UserName,
            Posts = GetPosts(user)
        };
    }
}