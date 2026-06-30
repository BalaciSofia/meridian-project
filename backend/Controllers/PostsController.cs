using backend.Models;
using backend.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly PostService _postService;

        public PostsController(PostService postService)
        {
            _postService = postService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Post>>> GetPosts()
        {
            var res = await _postService.GetAllPosts();
            return res;
        }

        [HttpPost]
        public async Task<IActionResult> AddPost(Post post)
        {
            await _postService.AddPost(post);
            return NoContent();
        }
    }
}
