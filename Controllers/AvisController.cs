using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class AvisController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public AvisController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/Avis
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Avis>>> GetAvis()
    {
        return await _context.Avis.ToListAsync();
    }

    // GET: api/Avis/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Avis>> GetAvi(int id)
    {
        var avi = await _context.Avis.FindAsync(id);

        if (avi == null)
        {
            return NotFound();
        }

        return avi;
    }

    // GET: api/Avis/ByRestaurant/5
    [HttpGet("ByRestaurant/{restaurantId}")]
    public async Task<ActionResult<IEnumerable<Avis>>> GetAvisByRestaurant(int restaurantId)
    {
        return await _context.Avis
            .Where(a => a.IdRestaurant == restaurantId)
            .ToListAsync();
    }

    // GET: api/Avis/ByUser/5
    [HttpGet("ByUser/{userId}")]
    public async Task<ActionResult<IEnumerable<Avis>>> GetAvisByUser(int userId)
    {
        return await _context.Avis
            .Where(a => a.IdUtilisateur == userId)
            .ToListAsync();
    }

    // PUT: api/Avis/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutAvi(int id, Avis avi)
    {
        if (id != avi.IdAvis)
        {
            return BadRequest();
        }

        _context.Entry(avi).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!AviExists(id))
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

    // POST: api/Avis
    [HttpPost]
    public async Task<ActionResult<Avis>> PostAvi(Avis avi)
    {
        _context.Avis.Add(avi);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetAvi", new { id = avi.IdAvis }, avi);
    }

    // DELETE: api/Avis/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAvi(int id)
    {
        var avi = await _context.Avis.FindAsync(id);
        if (avi == null)
        {
            return NotFound();
        }

        _context.Avis.Remove(avi);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool AviExists(int id)
    {
        return _context.Avis.Any(e => e.IdAvis == id);
    }
}