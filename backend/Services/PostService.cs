using backend.DTOs;
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

        public async Task<IEnumerable<PostResponse>> GetAllPosts()
        {
            var posts = await _postRepository.GetAllPosts();

            return posts.Select(MapToPostResponse);
        }

        public async Task AddPost(CreatePostRequest request)
        {
            var post = new Post
            {
                Title = request.Title,
                Content = request.Content,
                ImageUrl = request.ImageUrl,
                CreatedByAccountId = request.CreatedByAccountId
            };

            await _postRepository.AddPost(post);
        }

        private static PostResponse MapToPostResponse(Post post)
        {
            return new PostResponse
            {
                Id = post.Id,
                Title = post.Title,
                Content = post.Content,
                ImageUrl = post.ImageUrl,
                CreatedAt = post.CreatedAt,
                CreatedByAccountId = post.CreatedByAccountId,
                CreatedByName = $"{post.CreatedByAccount.FirstName} {post.CreatedByAccount.LastName}".Trim()
            };
        }
    }
}
