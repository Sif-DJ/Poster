using Poster.Core.DTOs;
using Poster.Core.Models;

namespace Poster.Core;

public class Extensions
{
    public static List<PostDTO> GetPosts(User user)
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