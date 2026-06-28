using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Models;
using backend.Data;

[Route("api/[controller]")]
[ApiController]
public class EventParticipantsController : ControllerBase
{
    private readonly AppDbContext _context;
    public EventParticipantsController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/EventParticipant
    [HttpGet]
    public async Task<ActionResult<IEnumerable<EventParticipant>>> GetEventParticipant()
    {
        return await _context.EventParticipants.ToListAsync();
    }

    // GET: api/EventParticipant/5
    [HttpGet("{id}")]
    public async Task<ActionResult<EventParticipant>> GetEventParticipant(int id)
    {
        var eventparticipant = await _context.EventParticipants.FindAsync(id);

        if (eventparticipant == null)
        {
            return NotFound();
        }

        return eventparticipant;
    }

    // PUT: api/EventParticipant/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutEventParticipant(int? id, EventParticipant eventparticipant)
    {
        if (id != eventparticipant.Id)
        {
            return BadRequest();
        }

        _context.Entry(eventparticipant).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!EventParticipantExists(id))
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

    // POST: api/EventParticipant
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<EventParticipant>> PostEventParticipant(EventParticipant eventparticipant)
    {
        _context.EventParticipants.Add(eventparticipant);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetEventParticipant", new { id = eventparticipant.Id }, eventparticipant);
    }

    // DELETE: api/EventParticipant/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEventParticipant(int? id)
    {
        var eventparticipant = await _context.EventParticipants.FindAsync(id);
        if (eventparticipant == null)
        {
            return NotFound();
        }

        _context.EventParticipants.Remove(eventparticipant);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool EventParticipantExists(int? id)
    {
        return _context.EventParticipants.Any(e => e.Id == id);
    }
}
