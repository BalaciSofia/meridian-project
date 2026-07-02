using backend.DTOs;
using backend.Mapping;
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
        private readonly PostMapper _postMapper;
        private readonly ReactMapper _reactMapper;

        public PostsController(
            IPostService postService,
            IReactsService reactsService,
            PostMapper postMapper,
            ReactMapper reactMapper)
        {
            _postService = postService;
            _reactsService = reactsService;
            _postMapper = postMapper;
            _reactMapper = reactMapper;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PostResponse>>> GetPosts()
        {
            var posts = await _postService.GetAllPosts();
            var response = _postMapper.ToResponses(posts);

            return Ok(response);
        }

        [Authorize(Roles = "HR,Admin")]
        [HttpPost]
        public async Task<IActionResult> AddPost(CreatePostRequest request)
        {
            var post = _postMapper.ToEntity(request);

            await _postService.AddPost(post);
            return Created();
        }

        [Authorize]
        [HttpGet("{postId}/reacts")]
        public async Task<ActionResult<IEnumerable<ReactResponse>>> GetReactsForPost(int postId)
        {
            var reacts = await _reactsService.GetAllReactsForPost(postId);
            var response = _reactMapper.ToResponses(reacts);

            return Ok(response);
        }

        [Authorize]
        [HttpPost("{postId}/reacts")]
        public async Task<IActionResult> AddReact(int postId, CreateReactRequest request)
        {
            var react = _reactMapper.ToEntity(request);
            react.PostId = postId;

            await _reactsService.AddReact(react);
            return Created();
        }
    }
}
