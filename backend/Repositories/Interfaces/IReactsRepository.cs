using backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace backend.Repositories.Interfaces
{
    public interface IReactsRepository
    {
        Task AddReact(React react);
        Task<React?> FindReactByPostIdAndAccountId(int postId, int accountId);
        Task<IEnumerable<React>> GetAllReactsForPost(int postId);
        Task RemoveReact(int postId, int accountId);
        Task UpdateReact(React react);
    }
}