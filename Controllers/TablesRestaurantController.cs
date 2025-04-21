using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class TablesRestaurantController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public TablesRestaurantController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/TablesRestaurant
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TableRestaurant>>> GetTablesRestaurant()
    {
        return await _context.TablesRestaurant.ToListAsync();
    }

    // GET: api/TablesRestaurant/5
    [HttpGet("{id}")]
    public async Task<ActionResult<TableRestaurant>> GetTableRestaurant(int id)
    {
        var tableRestaurant = await _context.TablesRestaurant.FindAsync(id);

        if (tableRestaurant == null)
        {
            return NotFound();
        }

        return tableRestaurant;
    }

    // GET: api/TablesRestaurant/BySalle/5
    [HttpGet("BySalle/{salleId}")]
    public async Task<ActionResult<IEnumerable<TableRestaurant>>> GetTablesBySalle(int salleId)
    {
        return await _context.TablesRestaurant
            .Where(t => t.IdSalle == salleId)
            .ToListAsync();
    }

    // PUT: api/TablesRestaurant/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutTableRestaurant(int id, TableRestaurant tableRestaurant)
    {
        if (id != tableRestaurant.IdTable)
        {
            return BadRequest();
        }

        _context.Entry(tableRestaurant).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!TableRestaurantExists(id))
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

    // POST: api/TablesRestaurant
    [HttpPost]
    public async Task<ActionResult<TableRestaurant>> PostTableRestaurant(TableRestaurant tableRestaurant)
    {
        _context.TablesRestaurant.Add(tableRestaurant);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetTableRestaurant", new { id = tableRestaurant.IdTable }, tableRestaurant);
    }

    // DELETE: api/TablesRestaurant/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTableRestaurant(int id)
    {
        var tableRestaurant = await _context.TablesRestaurant.FindAsync(id);
        if (tableRestaurant == null)
        {
            return NotFound();
        }

        _context.TablesRestaurant.Remove(tableRestaurant);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool TableRestaurantExists(int id)
    {
        return _context.TablesRestaurant.Any(e => e.IdTable == id);
    }
}