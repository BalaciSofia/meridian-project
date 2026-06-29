using backend.Models;

namespace backend.Repositories
{
    public interface IAccountRepository
    {
        Task<Account?> GetByEmailWithRoleAndDepartmentAsync(string email);

        Task<Account?> GetByIdAsync(int id);

        Task AddAccountAsync(Account account);
    }
}
