using backend.Models;

namespace backend.Repositories.Interfaces
{
    public interface IWorkSchedulesRepository
    {
        Task<IEnumerable<WorkSchedule>> GetWorkSchedulesForUser(int accountId, DateTime? from, DateTime? to);

        Task<WorkSchedule?> GetWorkScheduleForDate(int accountId, DateTime date);

        Task AddWorkSchedule(WorkSchedule workSchedule);

        Task UpdateWorkSchedule(WorkSchedule workSchedule);
    }
}
