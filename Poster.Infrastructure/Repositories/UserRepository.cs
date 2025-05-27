using System.Security.Cryptography;
using System.Web.Helpers;
using Microsoft.EntityFrameworkCore;
using Poster.Core.DTOs;
using Poster.Core.Models;

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
        return await context.Users.AnyAsync(x => x.UserName == userName);
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
        var user = await context.Users.FirstOrDefaultAsync(x => x.UserName == username);
        if (user == null)
            return null;
        return Crypto.VerifyHashedPassword(user.Password, password) ? GetUserDTO(user) : null;
    }

    public async Task<UserDTO?> GetUserDTOByName(string userName)
    {
        var user = await context.Users.FirstOrDefaultAsync(x => x.UserName == userName);
        var dto = GetUserDTO(user);
        return dto;
    }

    private UserDTO? GetUserDTO(User user)
    {
        return new UserDTO
        {
            Username = user.UserName,
            Posts = GetPosts(user)
        };
    }

    private List<PostDTO> GetPosts(User user)
    {
        var list = new List<PostDTO>();
        foreach (var post in user.Posts)
        {
            var dto = new PostDTO
            {
                Id = post.Id,
                Title = post.Title,
                Content = post.Content,
                PublishDate = post.PublishDate,
                UserId = user.Id
            };
            list.Add(dto);
        }
        return list;
    }
}