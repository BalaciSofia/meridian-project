using backend.Models;
using backend.Repositories.Interfaces;
using backend.Services.Interfaces;

namespace backend.Services
{
    public class LookupsService : ILookupsService
    {
        private readonly ILookupsRepository _lookupsRepository;

        public LookupsService(ILookupsRepository lookupsRepository)
        {
            _lookupsRepository = lookupsRepository;
        }

        public async Task<IEnumerable<Role>> GetAllRoles()
        {
            return await _lookupsRepository.GetAllRoles();
        }

        public async Task<IEnumerable<Department>> GetAllDepartments()
        {
            return await _lookupsRepository.GetAllDepartments();
        }
    }
}
