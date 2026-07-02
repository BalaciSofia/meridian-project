using backend.DTOs;
using backend.Models;

namespace backend.Services.Interfaces
{
    public interface IAuthService
    {
        Task AddAccountAsync(Account account);

        Task<LoginResponse?> LoginAsync(string email, string password);
    }
}
