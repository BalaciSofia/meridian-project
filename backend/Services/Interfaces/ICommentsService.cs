using backend.Models;

namespace backend.Services.Interfaces
{
    public interface ICommentsService
    {
        Task AddComment(Comment comment);

        Task<IEnumerable<Comment>> GetAllCommentsForPost(int postId);
    }
}
