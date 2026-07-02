using backend.Models;
using backend.Repositories.Interfaces;
using backend.Services.Interfaces;

namespace backend.Services
{
    public class CommentsService : ICommentsService
    {
        private readonly ICommentsRepository _commentsRepository;

        public CommentsService(ICommentsRepository commentsRepository)
        {
            _commentsRepository = commentsRepository;
        }

        public async Task<IEnumerable<Comment>> GetAllCommentsForPost(int postId)
        {
            return await _commentsRepository.GetAllCommentsForPost(postId);
        }

        public async Task AddComment(Comment comment)
        {
            await _commentsRepository.AddComment(comment);
        }
    }
}
