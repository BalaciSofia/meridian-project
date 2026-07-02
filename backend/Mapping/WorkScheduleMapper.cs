using backend.DTOs;
using backend.Models;
using Riok.Mapperly.Abstractions;

namespace backend.Mapping
{
    [Mapper]
    public partial class WorkScheduleMapper
    {
        [MapperIgnoreTarget(nameof(WorkSchedule.Id))]
        [MapperIgnoreTarget(nameof(WorkSchedule.AccountId))]
        [MapperIgnoreTarget(nameof(WorkSchedule.Account))]
        public partial WorkSchedule ToEntity(SetWorkScheduleRequest request);

        [MapperIgnoreSource(nameof(WorkSchedule.Account))]
        public partial WorkScheduleResponse ToResponse(WorkSchedule workSchedule);

        public partial IEnumerable<WorkScheduleResponse> ToResponses(IEnumerable<WorkSchedule> workSchedules);
    }
}
