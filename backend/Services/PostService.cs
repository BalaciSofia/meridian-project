using backend.Models;
using backend.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace backend.Services
{
    public class PostService
    {
        private readonly IPostRepository _postRepository;

        public PostService(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public async Task<ActionResult<IEnumerable<Post>>> GetAllPosts()
        {
            return await _postRepository.GetAllPosts();
        }

        public async Task AddPost(Post post)
        {
            await _postRepository.AddPost(post);
        }
    }
}
