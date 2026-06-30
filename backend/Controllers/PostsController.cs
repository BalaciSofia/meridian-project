using backend.DTOs;
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
        private readonly ReactsService _reactsService;

        public PostsController(PostService postService, ReactsService reactsService)
        {
            this._postService = postService;
            this._reactsService = reactsService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Post>>> GetPosts()
        {
            var res = await _postService.GetAllPosts();
            return Ok(res);
        }

        [HttpPost]
        public async Task<IActionResult> AddPost(Post post)
        {
            await _postService.AddPost(post);
            return Created();
        }

        [HttpGet("/react/{id}")]
        public async Task<ActionResult<IEnumerable<React>>> GetReactsForPost(int postId)
        {
            var res= await _reactsService.GetAllReactsForPost(postId);
            return Ok(res);
        }

        [HttpPost("/react")]
        public async Task<IActionResult> AddReact(React react)
        {
            await _reactsService.AddReact(react);
            return Created();
        }

    }
}
