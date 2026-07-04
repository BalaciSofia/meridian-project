using backend.Models;

namespace backend.Repositories.Interfaces
{
    public interface ILookupsRepository
    {
        Task<IEnumerable<Role>> GetAllRoles();

        Task<IEnumerable<Department>> GetAllDepartments();
    }
}
