using backend.DTOs;

namespace backend.Services.Interfaces
{
    public interface IPostService
    {
        Task AddPost(CreatePostRequest request);

        Task<IEnumerable<PostResponse>> GetAllPosts();
    }
}
