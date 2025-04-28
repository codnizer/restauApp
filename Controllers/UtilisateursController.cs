using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace RestauApp
{
    [Authorize(Roles = "Admin")]
    public class UtilisateursController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UtilisateursController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Utilisateurs
        public async Task<IActionResult> Index()
        {
            var utilisateurs = await _context.Utilisateurs.ToListAsync();
            return View(utilisateurs);
        }

        // GET: Utilisateurs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var utilisateur = await _context.Utilisateurs
                .FirstOrDefaultAsync(m => m.IdUtilisateur == id);

            if (utilisateur == null)
                return NotFound();

            return View(utilisateur);
        }

        // GET: Utilisateurs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Utilisateurs/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdUtilisateur,Nom,Prenom,Email,Telephone,MotDePasse,ProgrammeFidelite")] Utilisateur utilisateur)
        {
            if (!ModelState.IsValid)
            {
                // Optional: Add model error logging here
                return View(utilisateur);
            }

            _context.Add(utilisateur);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Utilisateurs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var utilisateur = await _context.Utilisateurs.FindAsync(id);
            if (utilisateur == null)
                return NotFound();

            return View(utilisateur);
        }

        // POST: Utilisateurs/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdUtilisateur,Nom,Prenom,Email,Telephone,MotDePasse,ProgrammeFidelite")] Utilisateur utilisateur)
        {
            if (id != utilisateur.IdUtilisateur)
                return NotFound();

            if (!ModelState.IsValid)
                return View(utilisateur);

            try
            {
                _context.Update(utilisateur);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UtilisateurExists(utilisateur.IdUtilisateur))
                    return NotFound();
                else
                    throw;
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Utilisateurs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var utilisateur = await _context.Utilisateurs
                .FirstOrDefaultAsync(m => m.IdUtilisateur == id);

            if (utilisateur == null)
                return NotFound();

            return View(utilisateur);
        }

        // POST: Utilisateurs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var utilisateur = await _context.Utilisateurs.FindAsync(id);
            if (utilisateur != null)
            {
                _context.Utilisateurs.Remove(utilisateur);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool UtilisateurExists(int id)
        {
            return _context.Utilisateurs.Any(e => e.IdUtilisateur == id);
        }
    }
}
