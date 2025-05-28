using Microsoft.EntityFrameworkCore;
using Poster.Core.DTOs;
using Poster.Core.Models;
using static Poster.Core.Extensions;

namespace Poster.Infrastructure.Repositories;

public class PostRepository(PosterContext context)
{
    public async Task<List<PostDTO>> GetAllPosts()
    {
        List<PostDTO> posts = new List<PostDTO>();
        foreach (var post in context.Posts.Include(post => post.User))
        {
            var dto = new PostDTO
            {
                Id = post.Id,
                Title = post.Title,
                Content = post.Content,
                PublishDate = post.PublishDate,
                UserId = post.User.Id
            };
            posts.Add(dto);
        }

        posts.Reverse();
        return posts;
    }

    public async Task<List<PostDTO>> GetAllPostsByUserId(int userId)
    {
        List<PostDTO> posts = new List<PostDTO>();
        foreach (var post in context.Posts.Include(post => post.User))
        {
            var dto = new PostDTO
            {
                Id = post.Id,
                Title = post.Title,
                Content = post.Content,
                PublishDate = post.PublishDate,
                UserId = post.User.Id
            };
            posts.Add(dto);
        }
        return posts;
    }

    public async Task AddPost(UserDTO user, string title, string content)
    {
        Post post = new Post
        {
            Id = 0,
            Title = title,
            Content = content,
            PublishDate = DateTime.Now,
            User = await context.Users.FirstAsync(x => x.UserName == user.Username)
        };
        context.Posts.Add(post);
        await context.SaveChangesAsync();
    }
}