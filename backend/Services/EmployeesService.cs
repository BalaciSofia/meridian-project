using backend.Models;
using backend.Repositories.Interfaces;
using backend.Services.Interfaces;

namespace backend.Services
{
    public class EmployeesService : IEmployeesService
    {
        private readonly IAccountRepository _accountRepository;

        public EmployeesService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<IEnumerable<Account>> GetAccounts()
        {
            return await _accountRepository.GetAllAccounts();
        }

        public async Task<Account?> GetAccountById(int id)
        {
            return await _accountRepository.GetByIdAsync(id);
        }
    }
}
