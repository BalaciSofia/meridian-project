using backend.Models;

namespace backend.Services.Interfaces
{
    public interface IWorkSchedulesService
    {
        Task<IEnumerable<WorkSchedule>> GetWorkSchedulesForUser(int accountId, DateTime? from, DateTime? to);

        Task SetWorkSchedule(int accountId, WorkSchedule workSchedule);
    }
}
