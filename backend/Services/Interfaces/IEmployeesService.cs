using backend.DTOs;

namespace backend.Services.Interfaces
{
    public interface IEmployeesService
    {
        Task<EmployeeResponse?> GetAccountById(int id);

        Task<IEnumerable<EmployeeResponse>> GetAccounts();
    }
}
