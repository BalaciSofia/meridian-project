using backend.Models;

namespace backend.Services.Interfaces
{
    public interface ILookupsService
    {
        Task<IEnumerable<Role>> GetAllRoles();

        Task<IEnumerable<Department>> GetAllDepartments();
    }
}
