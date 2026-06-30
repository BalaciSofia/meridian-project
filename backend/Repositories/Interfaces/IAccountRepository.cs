using backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace backend.Repositories.Interfaces
{
    public interface IAccountRepository
    {
        Task<Account?> GetByEmailWithRoleAndDepartmentAsync(string email);

        Task<Account?> GetByIdAsync(int id);

        Task AddAccountAsync(Account account);

        Task<ActionResult<IEnumerable<Account>>> GetAllAccounts();
    }
}
