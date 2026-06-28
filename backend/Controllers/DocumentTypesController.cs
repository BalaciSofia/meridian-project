using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Models;
using backend.Data;

[Route("api/[controller]")]
[ApiController]
public class DocumentTypesController : ControllerBase
{
    private readonly AppDbContext _context;
    public DocumentTypesController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/DocumentType
    [HttpGet]
    public async Task<ActionResult<IEnumerable<DocumentType>>> GetDocumentType()
    {
        return await _context.DocumentTypes.ToListAsync();
    }

    // GET: api/DocumentType/5
    [HttpGet("{id}")]
    public async Task<ActionResult<DocumentType>> GetDocumentType(int id)
    {
        var documenttype = await _context.DocumentTypes.FindAsync(id);

        if (documenttype == null)
        {
            return NotFound();
        }

        return documenttype;
    }

    // PUT: api/DocumentType/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutDocumentType(int? id, DocumentType documenttype)
    {
        if (id != documenttype.Id)
        {
            return BadRequest();
        }

        _context.Entry(documenttype).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!DocumentTypeExists(id))
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

    // POST: api/DocumentType
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<DocumentType>> PostDocumentType(DocumentType documenttype)
    {
        _context.DocumentTypes.Add(documenttype);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetDocumentType", new { id = documenttype.Id }, documenttype);
    }

    // DELETE: api/DocumentType/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDocumentType(int? id)
    {
        var documenttype = await _context.DocumentTypes.FindAsync(id);
        if (documenttype == null)
        {
            return NotFound();
        }

        _context.DocumentTypes.Remove(documenttype);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool DocumentTypeExists(int? id)
    {
        return _context.DocumentTypes.Any(e => e.Id == id);
    }
}
