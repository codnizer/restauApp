using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace RestauApp
{
    public class AvisController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AvisController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Avis
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Avis.Include(a => a.Restaurant).Include(a => a.Utilisateur);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Avis/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var avis = await _context.Avis
                .Include(a => a.Restaurant)
                .Include(a => a.Utilisateur)
                .FirstOrDefaultAsync(m => m.IdAvis == id);
            if (avis == null)
            {
                return NotFound();
            }

            return View(avis);
        }

        // GET: Avis/Create
        public IActionResult Create()
        {
            ViewData["IdRestaurant"] = new SelectList(_context.Restaurants, "IdRestaurant", "Adresse");
            ViewData["IdUtilisateur"] = new SelectList(_context.Utilisateurs, "IdUtilisateur", "Email");
            return View();
        }

        // POST: Avis/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdAvis,Note,Commentaire,DateAvis,IdUtilisateur,IdRestaurant")] Avis avis)
        {
            if (ModelState.IsValid)
            {
                _context.Add(avis);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdRestaurant"] = new SelectList(_context.Restaurants, "IdRestaurant", "Adresse", avis.IdRestaurant);
            ViewData["IdUtilisateur"] = new SelectList(_context.Utilisateurs, "IdUtilisateur", "Email", avis.IdUtilisateur);
            return View(avis);
        }

        // GET: Avis/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var avis = await _context.Avis.FindAsync(id);
            if (avis == null)
            {
                return NotFound();
            }
            ViewData["IdRestaurant"] = new SelectList(_context.Restaurants, "IdRestaurant", "Adresse", avis.IdRestaurant);
            ViewData["IdUtilisateur"] = new SelectList(_context.Utilisateurs, "IdUtilisateur", "Email", avis.IdUtilisateur);
            return View(avis);
        }

        // POST: Avis/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdAvis,Note,Commentaire,DateAvis,IdUtilisateur,IdRestaurant")] Avis avis)
        {
            if (id != avis.IdAvis)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(avis);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AvisExists(avis.IdAvis))
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
            ViewData["IdRestaurant"] = new SelectList(_context.Restaurants, "IdRestaurant", "Adresse", avis.IdRestaurant);
            ViewData["IdUtilisateur"] = new SelectList(_context.Utilisateurs, "IdUtilisateur", "Email", avis.IdUtilisateur);
            return View(avis);
        }

        // GET: Avis/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var avis = await _context.Avis
                .Include(a => a.Restaurant)
                .Include(a => a.Utilisateur)
                .FirstOrDefaultAsync(m => m.IdAvis == id);
            if (avis == null)
            {
                return NotFound();
            }

            return View(avis);
        }

        // POST: Avis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var avis = await _context.Avis.FindAsync(id);
            if (avis != null)
            {
                _context.Avis.Remove(avis);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AvisExists(int id)
        {
            return _context.Avis.Any(e => e.IdAvis == id);
        }
    }
}
