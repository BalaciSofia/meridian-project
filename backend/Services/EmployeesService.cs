using backend.Models;
using backend.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace backend.Services
{
    public class EmployeesService
    {
        private readonly IAccountRepository _accountRepository;

        public EmployeesService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<ActionResult<IEnumerable<Account>>> GetAccounts()
        {
            return await _accountRepository.GetAllAccounts();
        }
    }
}
