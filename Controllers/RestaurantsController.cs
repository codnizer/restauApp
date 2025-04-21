using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace RestauApp
{
    public class RestaurantsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<RestaurantsController> _logger;

        public RestaurantsController(ApplicationDbContext context, ILogger<RestaurantsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: Restaurants
        public async Task<IActionResult> Index()
        {
            return View(await _context.Restaurants.ToListAsync());
        }

        // GET: Restaurants/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var restaurant = await _context.Restaurants
                .FirstOrDefaultAsync(m => m.IdRestaurant == id);

            if (restaurant == null)
                return NotFound();

            return View(restaurant);
        }

        // GET: Restaurants/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Restaurants/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdRestaurant,Nom,Adresse,Telephone,Email,Horaires,Description,Photo,Menu")] Restaurant restaurant)
        {
            if (!ModelState.IsValid)
            {
                // Log all validation errors
                foreach (var modelState in ModelState)
                {
                    foreach (var error in modelState.Value.Errors)
                    {
                        _logger.LogWarning($"Validation error in '{modelState.Key}': {error.ErrorMessage}");
                    }
                }

                return View(restaurant);
            }

            _context.Add(restaurant);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Restaurants/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var restaurant = await _context.Restaurants.FindAsync(id);
            if (restaurant == null)
                return NotFound();

            return View(restaurant);
        }

        // POST: Restaurants/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdRestaurant,Nom,Adresse,Telephone,Email,Horaires,Description,Photo,Menu")] Restaurant restaurant)
        {
            if (id != restaurant.IdRestaurant)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(restaurant);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RestaurantExists(restaurant.IdRestaurant))
                        return NotFound();
                    else
                        throw;
                }

                return RedirectToAction(nameof(Index));
            }

            return View(restaurant);
        }

        // GET: Restaurants/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var restaurant = await _context.Restaurants
                .FirstOrDefaultAsync(m => m.IdRestaurant == id);

            if (restaurant == null)
                return NotFound();

            return View(restaurant);
        }

        // POST: Restaurants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var restaurant = await _context.Restaurants.FindAsync(id);
            if (restaurant != null)
                _context.Restaurants.Remove(restaurant);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RestaurantExists(int id)
        {
            return _context.Restaurants.Any(e => e.IdRestaurant == id);
        }
    }
}
