using Microsoft.EntityFrameworkCore;

namespace Poster.Infrastructure.Repositories;

public class UserRepository(PosterContext context)
{
    public async Task<int> GetAmountOfUsers()
    {
        var list = await context.Users.ToListAsync();
        return list.Count();
    }
}