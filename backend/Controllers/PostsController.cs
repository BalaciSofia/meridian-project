using backend.DTOs;
using backend.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IPostService _postService;
        private readonly IReactsService _reactsService;

        public PostsController(IPostService postService, IReactsService reactsService)
        {
            _postService = postService;
            _reactsService = reactsService;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PostResponse>>> GetPosts()
        {
            var response = await _postService.GetAllPosts();
            return Ok(response);
        }

        [Authorize(Roles = "HR,Admin")]
        [HttpPost]
        public async Task<IActionResult> AddPost(CreatePostRequest request)
        {
            await _postService.AddPost(request);
            return Created();
        }

        [Authorize]
        [HttpGet("{postId}/reacts")]
        public async Task<ActionResult<IEnumerable<ReactResponse>>> GetReactsForPost(int postId)
        {
            var response = await _reactsService.GetAllReactsForPost(postId);
            return Ok(response);
        }

        [Authorize]
        [HttpPost("{postId}/reacts")]
        public async Task<IActionResult> AddReact(int postId, CreateReactRequest request)
        {
            await _reactsService.AddReact(postId, request);
            return Created();
        }
    }
}
