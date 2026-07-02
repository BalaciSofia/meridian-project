using backend.Data;
using backend.Models;
using backend.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories
{
    public class WorkSchedulesRepository : IWorkSchedulesRepository
    {
        private readonly AppDbContext _context;

        public WorkSchedulesRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<WorkSchedule>> GetWorkSchedulesForUser(int accountId, DateTime? from, DateTime? to)
        {
            var query = _context.WorkSchedules
                .Where(ws => ws.AccountId == accountId);

            if (from.HasValue)
            {
                query = query.Where(ws => ws.Date >= from.Value.Date);
            }

            if (to.HasValue)
            {
                query = query.Where(ws => ws.Date <= to.Value.Date);
            }

            return await query
                .OrderBy(ws => ws.Date)
                .ToListAsync();
        }

        public async Task<WorkSchedule?> GetWorkScheduleForDate(int accountId, DateTime date)
        {
            var scheduleDate = date.Date;

            return await _context.WorkSchedules
                .FirstOrDefaultAsync(ws => ws.AccountId == accountId && ws.Date == scheduleDate);
        }

        public async Task AddWorkSchedule(WorkSchedule workSchedule)
        {
            await _context.WorkSchedules.AddAsync(workSchedule);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateWorkSchedule(WorkSchedule workSchedule)
        {
            _context.WorkSchedules.Update(workSchedule);
            await _context.SaveChangesAsync();
        }
    }
}
