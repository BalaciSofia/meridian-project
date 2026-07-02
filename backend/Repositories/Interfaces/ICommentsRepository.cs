using backend.Models;

namespace backend.Repositories.Interfaces
{
    public interface ICommentsRepository
    {
        Task AddComment(Comment comment);
        Task<IEnumerable<Comment>> GetAllCommentsForPost(int postId);
    }
}