using backend.Models;

namespace backend.Repositories.Interfaces
{
    public interface IPostRepository
    {
        Task<IEnumerable<Post>> GetAllPosts();

        Task AddPost(Post post);
    }
}
