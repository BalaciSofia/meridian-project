using backend.DTOs;
using backend.Mapping;
using backend.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class WorkSchedulesController : ControllerBase
    {
        private readonly IWorkSchedulesService _workSchedulesService;
        private readonly WorkScheduleMapper _workScheduleMapper;

        public WorkSchedulesController(
            IWorkSchedulesService workSchedulesService,
            WorkScheduleMapper workScheduleMapper)
        {
            _workSchedulesService = workSchedulesService;
            _workScheduleMapper = workScheduleMapper;
        }

        [HttpGet("my")]
        public async Task<ActionResult<IEnumerable<WorkScheduleResponse>>> GetMyWorkSchedules(
            [FromQuery] DateTime? from,
            [FromQuery] DateTime? to)
        {
            var accountId = GetCurrentAccountId();
            var workSchedules = await _workSchedulesService.GetWorkSchedulesForUser(accountId, from, to);
            var response = _workScheduleMapper.ToResponses(workSchedules);

            return Ok(response);
        }

        [HttpPut("my")]
        public async Task<IActionResult> SetMyWorkSchedule(SetWorkScheduleRequest request)
        {
            var accountId = GetCurrentAccountId();
            var workSchedule = _workScheduleMapper.ToEntity(request);

            try
            {
                await _workSchedulesService.SetWorkSchedule(accountId, workSchedule);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }

            return NoContent();
        }

        private int GetCurrentAccountId()
        {
            return int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        }
    }
}
