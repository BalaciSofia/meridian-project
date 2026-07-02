using backend.DTOs;

namespace backend.Services.Interfaces
{
    public interface IReactsService
    {
        Task AddReact(int postId, CreateReactRequest request);

        Task<IEnumerable<ReactResponse>> GetAllReactsForPost(int postId);
    }
}
