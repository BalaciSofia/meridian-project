using backend.Models;

namespace backend.Services.Interfaces
{
    public interface IPostService
    {
        Task AddPost(Post post);
        Task<IEnumerable<Post>> GetAllPosts();
    }
}