using backend.DTOs;
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

        public async Task<IEnumerable<EmployeeResponse>> GetAccounts()
        {
            var accounts = await _accountRepository.GetAllAccounts();

            return accounts.Select(MapToEmployeeResponse);
        }

        public async Task<EmployeeResponse?> GetAccountById(int id)
        {
            var account = await _accountRepository.GetByIdAsync(id);

            if (account == null)
            {
                return null;
            }

            return MapToEmployeeResponse(account);
        }

        private static EmployeeResponse MapToEmployeeResponse(Account account)
        {
            return new EmployeeResponse
            {
                Id = account.Id,
                Email = account.Email,
                FirstName = account.FirstName,
                LastName = account.LastName,
                Role = account.Role.RoleName,
                Department = account.Department.DepartmentName,
                MustChangePassword = account.MustChangePassword
            };
        }
    }
}
