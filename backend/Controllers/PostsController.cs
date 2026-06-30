using backend.DTOs;
using backend.Models;
using backend.Services;
using backend.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
            this._postService = postService;
            this._reactsService = reactsService;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Post>>> GetPosts()
        {
            var res = await _postService.GetAllPosts();
            return Ok(res);
        }

        [Authorize(Roles = "HR,Admin")]
        [HttpPost]
        public async Task<IActionResult> AddPost(Post post)
        {
            await _postService.AddPost(post);
            return Created();
        }

        [Authorize]
        [HttpGet("/{id}/reacts")]
        public async Task<ActionResult<IEnumerable<React>>> GetReactsForPost(int id)
        {
            var res= await _reactsService.GetAllReactsForPost(id);
            return Ok(res);
        }

        [Authorize]
        [HttpPost("/react")]
        public async Task<IActionResult> AddReact(React react)
        {
            await _reactsService.AddReact(react);
            return Created();
        }

    }
}
