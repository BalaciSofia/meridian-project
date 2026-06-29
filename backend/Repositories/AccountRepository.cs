using backend.Data;
using Microsoft.EntityFrameworkCore;
using backend.Models;

namespace backend.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly AppDbContext _context;

        public AccountRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Account?> GetByEmailWithRoleAndDepartmentAsync(string email)
        {
            return await _context.Accounts
                .Include(a => a.Role)
                .Include(a => a.Department)
                .FirstOrDefaultAsync(a => a.Email == email);
        }

        public async Task<Account?> GetByIdAsync(int id)
        {
            return await _context.Accounts.FindAsync(id);
        }
    }
}
