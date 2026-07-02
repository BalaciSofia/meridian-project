using backend.Models;
using backend.Repositories.Interfaces;
using backend.Services.Interfaces;

namespace backend.Services
{
    public class WorkSchedulesService : IWorkSchedulesService
    {
        private readonly IWorkSchedulesRepository _workSchedulesRepository;

        public WorkSchedulesService(IWorkSchedulesRepository workSchedulesRepository)
        {
            _workSchedulesRepository = workSchedulesRepository;
        }

        public async Task<IEnumerable<WorkSchedule>> GetWorkSchedulesForUser(int accountId, DateTime? from, DateTime? to)
        {
            return await _workSchedulesRepository.GetWorkSchedulesForUser(accountId, from, to);
        }

        public async Task SetWorkSchedule(int accountId, WorkSchedule workSchedule)
        {
            var scheduleDate = workSchedule.Date.Date;
            await ValidateRemoteDaysLimit(accountId, workSchedule, scheduleDate);

            var existingWorkSchedule = await _workSchedulesRepository.GetWorkScheduleForDate(accountId, scheduleDate);

            if (existingWorkSchedule == null)
            {
                workSchedule.AccountId = accountId;
                workSchedule.Date = scheduleDate;

                await _workSchedulesRepository.AddWorkSchedule(workSchedule);
                return;
            }

            existingWorkSchedule.WorkMode = workSchedule.WorkMode;
            await _workSchedulesRepository.UpdateWorkSchedule(existingWorkSchedule);
        }

        private async Task ValidateRemoteDaysLimit(int accountId, WorkSchedule workSchedule, DateTime scheduleDate)
        {
            if (!IsRemote(workSchedule.WorkMode))
            {
                return;
            }

            var weekStart = GetWeekStart(scheduleDate);
            var weekEnd = weekStart.AddDays(6);
            var schedulesForWeek = await _workSchedulesRepository.GetWorkSchedulesForUser(accountId, weekStart, weekEnd);

            var remoteDaysCount = schedulesForWeek
                .Where(ws => ws.Date.Date != scheduleDate)
                .Count(ws => IsRemote(ws.WorkMode));

            if (remoteDaysCount >= 2)
            {
                throw new InvalidOperationException("You can only select 2 remote days per week.");
            }
        }

        private static DateTime GetWeekStart(DateTime date)
        {
            var difference = ((int)date.DayOfWeek - (int)DayOfWeek.Monday + 7) % 7;
            return date.Date.AddDays(-difference);
        }

        private static bool IsRemote(string workMode)
        {
            return string.Equals(workMode, "remote", StringComparison.OrdinalIgnoreCase);
        }
    }
}
