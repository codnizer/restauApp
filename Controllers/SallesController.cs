using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace RestauApp
{
    public class SallesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<SallesController> _logger;

        // Constructor injecting both context and logger
        public SallesController(ApplicationDbContext context, ILogger<SallesController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: Salles
        public async Task<IActionResult> Index()
        {
            try
            {
                var applicationDbContext = _context.Salles.Include(s => s.Restaurant);
                return View(await applicationDbContext.ToListAsync());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching the list of Salles.");
                return View("Error");
            }
        }

        // GET: Salles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                _logger.LogWarning("Details page was accessed with null Id.");
                return NotFound();
            }

            try
            {
                var salle = await _context.Salles
                    .Include(s => s.Restaurant)
                    .FirstOrDefaultAsync(m => m.IdSalle == id);

                if (salle == null)
                {
                    _logger.LogWarning($"Salle with Id {id} not found.");
                    return NotFound();
                }

                return View(salle);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching details of Salle.");
                return View("Error");
            }
        }

        // GET: Salles/Create
        public IActionResult Create()
        {
            // Ensure ViewData contains a list of restaurants for the dropdown
            try
             
               
{
    // Ensure ViewData contains a list of restaurants for the dropdown
    ViewData["IdRestaurant"] = new SelectList(_context.Restaurants, "IdRestaurant", "Nom"); // Replace "Nom" with the property you want to display
    return View();
}
               
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching restaurants for the dropdown.");
                return View("Error");
            }
        }

        // POST: Salles/Create
[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Create([Bind("IdSalle,Nom,Capacite,Type,IdRestaurant")] Salle salle)
{
    if (ModelState.IsValid)
    {
        try
        {
            _context.Add(salle);
            await _context.SaveChangesAsync();
            _logger.LogInformation($"Salle with Id {salle.IdSalle} created successfully.");
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while creating a new Salle.");
            ModelState.AddModelError("", "An error occurred while saving the data. Please try again.");
        }
    }
    else
    {
        _logger.LogWarning("Invalid model state when creating a new Salle.");
        foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
        {
            _logger.LogWarning(error.ErrorMessage);
        }
    }

    // Ensure ViewData contains a list of restaurants for the dropdown again in case of error
    ViewData["IdRestaurant"] = new SelectList(_context.Restaurants, "IdRestaurant", "Adresse", salle.IdRestaurant);
    return View(salle);
}


        // GET: Salles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                _logger.LogWarning("Edit page was accessed with null Id.");
                return NotFound();
            }

            try
            {
                var salle = await _context.Salles.FindAsync(id);
                if (salle == null)
                {
                    _logger.LogWarning($"Salle with Id {id} not found for editing.");
                    return NotFound();
                }

                // Ensure ViewData contains a list of restaurants for the dropdown
                ViewData["IdRestaurant"] = new SelectList(_context.Restaurants, "IdRestaurant", "Adresse", salle.IdRestaurant);
                return View(salle);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching Salle for editing.");
                return View("Error");
            }
        }

        // POST: Salles/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdSalle,Nom,Capacite,Type,IdRestaurant")] Salle salle)
        {
            if (id != salle.IdSalle)
            {
                _logger.LogWarning($"Edit action: IdSalle mismatch. Expected {id}, got {salle.IdSalle}.");
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(salle);
                    await _context.SaveChangesAsync();
                    _logger.LogInformation($"Salle with Id {id} updated successfully.");
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SalleExists(salle.IdSalle))
                    {
                        _logger.LogError($"Salle with Id {salle.IdSalle} not found during update.");
                        return NotFound();
                    }
                    else
                    {
                        _logger.LogError("Concurrency error occurred while updating Salle.");
                        throw;
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error occurred while updating Salle.");
                    ModelState.AddModelError("", "An error occurred while saving the data. Please try again.");
                }
            }

            _logger.LogWarning("Invalid model state when editing Salle.");
            // Ensure ViewData contains a list of restaurants for the dropdown again in case of error
            ViewData["IdRestaurant"] = new SelectList(_context.Restaurants, "IdRestaurant", "Adresse", salle.IdRestaurant);
            return View(salle);
        }

        // GET: Salles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                _logger.LogWarning("Delete page was accessed with null Id.");
                return NotFound();
            }

            try
            {
                var salle = await _context.Salles
                    .Include(s => s.Restaurant)
                    .FirstOrDefaultAsync(m => m.IdSalle == id);

                if (salle == null)
                {
                    _logger.LogWarning($"Salle with Id {id} not found for deletion.");
                    return NotFound();
                }

                return View(salle);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching Salle for deletion.");
                return View("Error");
            }
        }

        // POST: Salles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var salle = await _context.Salles.FindAsync(id);
                if (salle != null)
                {
                    _context.Salles.Remove(salle);
                    await _context.SaveChangesAsync();
                    _logger.LogInformation($"Salle with Id {id} deleted successfully.");
                }
                else
                {
                    _logger.LogWarning($"Salle with Id {id} not found for deletion.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting Salle.");
            }
            return RedirectToAction(nameof(Index));
        }

        private bool SalleExists(int id)
        {
            return _context.Salles.Any(e => e.IdSalle == id);
        }
    }
}
