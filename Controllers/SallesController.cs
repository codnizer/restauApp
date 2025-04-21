using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class SallesController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public SallesController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/Salles
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Salle>>> GetSalles()
    {
        return await _context.Salles.ToListAsync();
    }

    // GET: api/Salles/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Salle>> GetSalle(int id)
    {
        var salle = await _context.Salles.FindAsync(id);

        if (salle == null)
        {
            return NotFound();
        }

        return salle;
    }

    // GET: api/Salles/ByRestaurant/5
    [HttpGet("ByRestaurant/{restaurantId}")]
    public async Task<ActionResult<IEnumerable<Salle>>> GetSallesByRestaurant(int restaurantId)
    {
        return await _context.Salles
            .Where(s => s.IdRestaurant == restaurantId)
            .ToListAsync();
    }

    // PUT: api/Salles/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutSalle(int id, Salle salle)
    {
        if (id != salle.IdSalle)
        {
            return BadRequest();
        }

        _context.Entry(salle).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!SalleExists(id))
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

    // POST: api/Salles
    [HttpPost]
    public async Task<ActionResult<Salle>> PostSalle(Salle salle)
    {
        _context.Salles.Add(salle);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetSalle", new { id = salle.IdSalle }, salle);
    }

    // DELETE: api/Salles/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSalle(int id)
    {
        var salle = await _context.Salles.FindAsync(id);
        if (salle == null)
        {
            return NotFound();
        }

        _context.Salles.Remove(salle);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool SalleExists(int id)
    {
        return _context.Salles.Any(e => e.IdSalle == id);
    }
}