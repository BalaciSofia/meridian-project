using backend.DTOs;

namespace backend.Services.Interfaces
{
    public interface IAuthService
    {
        Task AddAccountAsync(CreateAccountRequest request);
        Task<LoginResponse?> LoginAsync(LoginRequest request);
    }
}