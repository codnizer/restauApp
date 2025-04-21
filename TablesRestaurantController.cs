using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace RestauApp
{
    public class TablesRestaurantController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TablesRestaurantController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TablesRestaurant
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.TablesRestaurant.Include(t => t.Salle);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: TablesRestaurant/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tableRestaurant = await _context.TablesRestaurant
                .Include(t => t.Salle)
                .FirstOrDefaultAsync(m => m.IdTable == id);
            if (tableRestaurant == null)
            {
                return NotFound();
            }

            return View(tableRestaurant);
        }

        // GET: TablesRestaurant/Create
        public IActionResult Create()
        {
            ViewData["IdSalle"] = new SelectList(_context.Salles, "IdSalle", "Nom");
            return View();
        }

        // POST: TablesRestaurant/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdTable,Numero,IdSalle")] TableRestaurant tableRestaurant)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tableRestaurant);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdSalle"] = new SelectList(_context.Salles, "IdSalle", "Nom", tableRestaurant.IdSalle);
            return View(tableRestaurant);
        }

        // GET: TablesRestaurant/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tableRestaurant = await _context.TablesRestaurant.FindAsync(id);
            if (tableRestaurant == null)
            {
                return NotFound();
            }
            ViewData["IdSalle"] = new SelectList(_context.Salles, "IdSalle", "Nom", tableRestaurant.IdSalle);
            return View(tableRestaurant);
        }

        // POST: TablesRestaurant/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdTable,Numero,IdSalle")] TableRestaurant tableRestaurant)
        {
            if (id != tableRestaurant.IdTable)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tableRestaurant);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TableRestaurantExists(tableRestaurant.IdTable))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdSalle"] = new SelectList(_context.Salles, "IdSalle", "Nom", tableRestaurant.IdSalle);
            return View(tableRestaurant);
        }

        // GET: TablesRestaurant/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tableRestaurant = await _context.TablesRestaurant
                .Include(t => t.Salle)
                .FirstOrDefaultAsync(m => m.IdTable == id);
            if (tableRestaurant == null)
            {
                return NotFound();
            }

            return View(tableRestaurant);
        }

        // POST: TablesRestaurant/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tableRestaurant = await _context.TablesRestaurant.FindAsync(id);
            if (tableRestaurant != null)
            {
                _context.TablesRestaurant.Remove(tableRestaurant);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TableRestaurantExists(int id)
        {
            return _context.TablesRestaurant.Any(e => e.IdTable == id);
        }
    }
}
