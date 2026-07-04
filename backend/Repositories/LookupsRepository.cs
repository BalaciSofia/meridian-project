using backend.Data;
using backend.Models;
using backend.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories
{
    public class LookupsRepository : ILookupsRepository
    {
        private readonly AppDbContext _context;

        public LookupsRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Role>> GetAllRoles()
        {
            return await _context.Roles
                .OrderBy(r => r.Id)
                .ToListAsync();
        }

        public async Task<IEnumerable<Department>> GetAllDepartments()
        {
            return await _context.Departments
                .OrderBy(d => d.Id)
                .ToListAsync();
        }
    }
}
