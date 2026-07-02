using backend.Models;
using backend.Repositories.Interfaces;
using backend.Services.Interfaces;

namespace backend.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;

        public PostService(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public async Task<IEnumerable<Post>> GetAllPosts()
        {
            return await _postRepository.GetAllPosts();
        }

        public async Task AddPost(Post post)
        {
            await _postRepository.AddPost(post);
        }
    }
}
