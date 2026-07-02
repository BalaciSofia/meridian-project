using backend.DTOs;
using backend.Mapping;
using backend.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IPostService _postService;
        private readonly IReactsService _reactsService;
        private readonly ICommentsService _commentsService;
        private readonly PostMapper _postMapper;
        private readonly ReactMapper _reactMapper;
        private readonly CommentMapper _commentMapper;

        public PostsController(
            IPostService postService,
            IReactsService reactsService,
            ICommentsService commentsService,
            PostMapper postMapper,
            ReactMapper reactMapper,
            CommentMapper commentMapper)
        {
            _postService = postService;
            _reactsService = reactsService;
            _commentsService = commentsService;
            _postMapper = postMapper;
            _reactMapper = reactMapper;
            _commentMapper = commentMapper;
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
            var accountId = GetCurrentAccountId();
            var post = _postMapper.ToEntity(request);
            post.CreatedByAccountId = accountId;

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
            var accountId = GetCurrentAccountId();
            var react = _reactMapper.ToEntity(request);
            react.PostId = postId;
            react.AccountId = accountId;

            await _reactsService.AddReact(react);
            return Created();
        }

        [Authorize]
        [HttpGet("{postId}/comments")]
        public async Task<ActionResult<IEnumerable<CommentResponse>>> GetCommentsForPost(int postId)
        {
            var comments = await _commentsService.GetAllCommentsForPost(postId);
            var response = _commentMapper.ToResponses(comments);

            return Ok(response);
        }

        [Authorize]
        [HttpPost("{postId}/comments")]
        public async Task<IActionResult> AddComment(int postId, CreateCommentRequest request)
        {
            var accountId = GetCurrentAccountId();
            var comment = _commentMapper.ToEntity(request);
            comment.PostId = postId;
            comment.AccountId = accountId;

            await _commentsService.AddComment(comment);
            return Created();
        }

        private int GetCurrentAccountId()
        {
            return int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        }
    }
}
