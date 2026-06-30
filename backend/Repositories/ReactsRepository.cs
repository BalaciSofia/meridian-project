using backend.Data;
using backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using Microsoft.Identity.Client;

namespace backend.Repositories
{
    public class ReactsRepository
    {
        private readonly AppDbContext _context;

        public ReactsRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ActionResult<IEnumerable<React>>> GetAllReactsForPost(int postId)
        {
            return await _context.Reacts
                   .Where(r => r.PostId == postId)
                   .ToListAsync();
        }
        public async Task<React?> FindReactByPostIdAndAccountId(int postId, int accountId)
        {
            return await _context.Reacts.FindAsync(postId, accountId);
        }

        public async Task AddReact(React react)
        {
                await _context.Reacts.AddAsync(react);
                await _context.SaveChangesAsync();
        }

        public async Task RemoveReact(int postId, int accountId)
        {
            await _context.Reacts
                .Where(r=>r.PostId == postId && r.AccountId == accountId)
                .ExecuteDeleteAsync();
        }

        public async Task UpdateReact(React react) {
            await _context.Reacts
                .Where(r => r.PostId == react.PostId && r.AccountId == react.AccountId)
                .ExecuteUpdateAsync(setters=>setters.SetProperty(r=>r.ReactType,react.ReactType));
        }


    }
}
