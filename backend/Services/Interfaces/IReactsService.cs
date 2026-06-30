using backend.Models;

namespace backend.Services.Interfaces
{
    public interface IReactsService
    {
        Task AddReact(React react);
        Task<IEnumerable<React>> GetAllReactsForPost(int postId);
    }
}