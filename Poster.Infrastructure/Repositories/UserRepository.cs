using Microsoft.EntityFrameworkCore;
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
            Password = password
        });
        await context.SaveChangesAsync();
    }

    public async Task<User?> Login(string username, string password)
    {
        var user = await context.Users.FirstOrDefaultAsync(x => x.UserName == username);
        if (user == null)
            return null;
        return user.Password.Equals(password) ? user : null;
    }
}