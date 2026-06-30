using backend.Models;

namespace backend.Services.Interfaces
{
    public interface IEmployeesService
    {
        Task<Account?> GetAccountById(int id);
        Task<IEnumerable<Account>> GetAccounts();
    }
}