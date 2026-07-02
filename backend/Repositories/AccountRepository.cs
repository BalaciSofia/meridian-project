using backend.Data;
using backend.Models;
using backend.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

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
            return await _context.Accounts
                .Include(a => a.Role)
                .Include(a => a.Department)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task AddAccountAsync(Account account)
        {
            await _context.Accounts.AddAsync(account);
            await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<Account>> GetAllAccounts()
        {
            return await _context.Accounts
                .Include(a => a.Role)
                .Include(a => a.Department)
                .ToListAsync();
        }

    }
}
