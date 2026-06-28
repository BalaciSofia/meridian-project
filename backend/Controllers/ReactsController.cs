using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Models;
using backend.Data;

[Route("api/[controller]")]
[ApiController]
public class ReactsController : ControllerBase
{
    private readonly AppDbContext _context;
    public ReactsController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/React
    [HttpGet]
    public async Task<ActionResult<IEnumerable<React>>> GetReact()
    {
        return await _context.Reacts.ToListAsync();
    }

    // GET: api/React/5
    [HttpGet("{id}")]
    public async Task<ActionResult<React>> GetReact(int id)
    {
        var react = await _context.Reacts.FindAsync(id);

        if (react == null)
        {
            return NotFound();
        }

        return react;
    }

    // PUT: api/React/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutReact(int? id, React react)
    {
        if (id != react.Id)
        {
            return BadRequest();
        }

        _context.Entry(react).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ReactExists(id))
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

    // POST: api/React
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<React>> PostReact(React react)
    {
        _context.Reacts.Add(react);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetReact", new { id = react.Id }, react);
    }

    // DELETE: api/React/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteReact(int? id)
    {
        var react = await _context.Reacts.FindAsync(id);
        if (react == null)
        {
            return NotFound();
        }

        _context.Reacts.Remove(react);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool ReactExists(int? id)
    {
        return _context.Reacts.Any(e => e.Id == id);
    }
}
