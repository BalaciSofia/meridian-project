using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Models;
using backend.Data;

[Route("api/[controller]")]
[ApiController]
public class WorkSchedulesController : ControllerBase
{
    private readonly AppDbContext _context;
    public WorkSchedulesController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/WorkSchedule
    [HttpGet]
    public async Task<ActionResult<IEnumerable<WorkSchedule>>> GetWorkSchedule()
    {
        return await _context.WorkSchedules.ToListAsync();
    }

    // GET: api/WorkSchedule/5
    [HttpGet("{id}")]
    public async Task<ActionResult<WorkSchedule>> GetWorkSchedule(int id)
    {
        var workschedule = await _context.WorkSchedules.FindAsync(id);

        if (workschedule == null)
        {
            return NotFound();
        }

        return workschedule;
    }

    // PUT: api/WorkSchedule/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutWorkSchedule(int? id, WorkSchedule workschedule)
    {
        if (id != workschedule.Id)
        {
            return BadRequest();
        }

        _context.Entry(workschedule).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!WorkScheduleExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    // POST: api/WorkSchedule
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<WorkSchedule>> PostWorkSchedule(WorkSchedule workschedule)
    {
        _context.WorkSchedules.Add(workschedule);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetWorkSchedule", new { id = workschedule.Id }, workschedule);
    }

    // DELETE: api/WorkSchedule/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteWorkSchedule(int? id)
    {
        var workschedule = await _context.WorkSchedules.FindAsync(id);
        if (workschedule == null)
        {
            return NotFound();
        }

        _context.WorkSchedules.Remove(workschedule);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool WorkScheduleExists(int? id)
    {
        return _context.WorkSchedules.Any(e => e.Id == id);
    }
}
