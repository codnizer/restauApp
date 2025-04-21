using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class EmployesController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public EmployesController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/Employes
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Employe>>> GetEmployes()
    {
        return await _context.Employes.ToListAsync();
    }

    // GET: api/Employes/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Employe>> GetEmploye(int id)
    {
        var employe = await _context.Employes.FindAsync(id);

        if (employe == null)
        {
            return NotFound();
        }

        return employe;
    }

    // GET: api/Employes/ByRestaurant/5
    [HttpGet("ByRestaurant/{restaurantId}")]
    public async Task<ActionResult<IEnumerable<Employe>>> GetEmployesByRestaurant(int restaurantId)
    {
        return await _context.Employes
            .Where(e => e.IdRestaurant == restaurantId)
            .ToListAsync();
    }

    // PUT: api/Employes/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutEmploye(int id, Employe employe)
    {
        if (id != employe.IdEmploye)
        {
            return BadRequest();
        }

        _context.Entry(employe).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!EmployeExists(id))
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

    // POST: api/Employes
    [HttpPost]
    public async Task<ActionResult<Employe>> PostEmploye(Employe employe)
    {
        _context.Employes.Add(employe);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetEmploye", new { id = employe.IdEmploye }, employe);
    }

    // DELETE: api/Employes/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEmploye(int id)
    {
        var employe = await _context.Employes.FindAsync(id);
        if (employe == null)
        {
            return NotFound();
        }

        _context.Employes.Remove(employe);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool EmployeExists(int id)
    {
        return _context.Employes.Any(e => e.IdEmploye == id);
    }
}