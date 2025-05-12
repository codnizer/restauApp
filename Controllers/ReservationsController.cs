using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace RestauApp
{
    [Authorize(Roles = "Admin,Serveur,Hotesse")]
    public class ReservationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReservationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Reservations
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Reservations.Include(r => r.Restaurant).Include(r => r.TableRestaurant).Include(r => r.Utilisateur);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Reservations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservations
                .Include(r => r.Restaurant)
                .Include(r => r.TableRestaurant)
                .Include(r => r.Utilisateur)
                .FirstOrDefaultAsync(m => m.IdReservation == id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        // GET: Reservations/Create
        public IActionResult Create()
        {
            ViewData["IdRestaurant"] = new SelectList(_context.Restaurants, "IdRestaurant", "Adresse");
            //ViewData["IdTable"] = new SelectList(new List<TableRestaurant>(), "IdTable", "IdTable"); // vide par d�faut
            ViewData["IdUtilisateur"] = new SelectList(_context.Utilisateurs, "IdUtilisateur", "Email");
            ViewData["IdTable"] = new SelectList(_context.TablesRestaurant, "IdTable", "IdTable");

            // Liste des statuts possibles
            ViewData["StatusList"] = new SelectList(new[] { "en attente", "confirm�e", "annul�e" });
            ViewData["ArriverList"] = new SelectList(new[] { "non arriv�", "arriv�", "en retard" });

            return View();
        }


        // POST: Reservations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdReservation,DateRes,Heure,NombrePersonnes,ServiceSpecial,Status,Arriver,IdUtilisateur,IdTable,IdRestaurant")] Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reservation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["IdRestaurant"] = new SelectList(_context.Restaurants, "IdRestaurant", "Adresse", reservation.IdRestaurant);
            ViewData["IdTable"] = new SelectList(_context.TablesRestaurant, "IdTable", "IdTable", reservation.IdTable);
            ViewData["IdUtilisateur"] = new SelectList(_context.Utilisateurs, "IdUtilisateur", "Email", reservation.IdUtilisateur);
            ViewData["StatusList"] = new SelectList(new[] { "en attente", "confirm�e", "annul�e" }, reservation.Status);
            ViewData["ArriverList"] = new SelectList(new[] { "non arriv�", "arriv�", "en retard" },reservation.Arriver);
            //ViewData["IdTable"] = GetTablesDisponibles(reservation.DateRes, reservation.Heure);

            return View(reservation);
        }

        // GET: Reservations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation == null)
            {
                return NotFound();
            }
            ViewData["IdRestaurant"] = new SelectList(_context.Restaurants, "IdRestaurant", "Adresse", reservation.IdRestaurant);
            ViewData["IdTable"] = new SelectList(_context.TablesRestaurant, "IdTable", "IdTable", reservation.IdTable);
            ViewData["IdUtilisateur"] = new SelectList(_context.Utilisateurs, "IdUtilisateur", "Email", reservation.IdUtilisateur);
            ViewData["StatusList"] = new SelectList(new[] { "en attente", "confirm�e", "annul�e" }, reservation.Status);
            ViewData["ArriverList"] = new SelectList(new[] { "non arriv�", "arriv�", "en retard" }, reservation.Arriver);

            //ViewData["IdTable"] = GetTablesDisponibles(reservation.DateRes, reservation.Heure);

            return View(reservation);
        }

        // POST: Reservations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdReservation,DateRes,Heure,NombrePersonnes,ServiceSpecial,Status,Arriver,IdUtilisateur,IdTable,IdRestaurant")] Reservation reservation)
        {
            if (id != reservation.IdReservation)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reservation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservationExists(reservation.IdReservation))
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
            ViewData["IdRestaurant"] = new SelectList(_context.Restaurants, "IdRestaurant", "Adresse", reservation.IdRestaurant);
            ViewData["IdTable"] = new SelectList(_context.TablesRestaurant, "IdTable", "IdTable", reservation.IdTable);
            ViewData["StatusList"] = new SelectList(new[] { "en attente", "confirm�e", "annul�e" }, reservation.Status);
            ViewData["IdUtilisateur"] = new SelectList(_context.Utilisateurs, "IdUtilisateur", "Email", reservation.IdUtilisateur);
            ViewData["ArriverList"] = new SelectList(new[] { "non arriv�", "arriv�", "en retard" }, reservation.Arriver);

            //ViewData["IdTable"] = GetTablesDisponibles(reservation.DateRes, reservation.Heure);

            return View(reservation);
        }

        // GET: Reservations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservations
                .Include(r => r.Restaurant)
                .Include(r => r.TableRestaurant)
                .Include(r => r.Utilisateur)
                .FirstOrDefaultAsync(m => m.IdReservation == id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        // POST: Reservations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation != null)
            {
                _context.Reservations.Remove(reservation);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReservationExists(int id)
        {
            return _context.Reservations.Any(e => e.IdReservation == id);
        }

        // POST: Reservations/Confirmer/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Confirmer(int id)
        {
            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation == null)
            {
                return NotFound();
            }

            if (reservation.Status == "en attente")
            {
                reservation.Status = "confirm�e";
                _context.Update(reservation);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        // POST: Reservations/Annuler/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Annuler(int id)
        {
            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation == null)
            {
                return NotFound();
            }

            if (reservation.Status == "en attente")
            {
                reservation.Status = "annul�e";
                _context.Update(reservation);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Arriver(int id)
        {
            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation == null)
            {
                return NotFound();
            }

            reservation.Arriver = "arriv�";
            _context.Update(reservation);
            await _context.SaveChangesAsync();


            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> EnRetard(int id)
        {
            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation == null)
            {
                return NotFound();
            }

            reservation.Arriver = "en retard";
            _context.Update(reservation);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public IActionResult NonArrive(int id)
        {
            var reservation = _context.Reservations.Find(id);
            if (reservation == null)
            {
                return NotFound();
            }

            reservation.Arriver = "non arriv�";
            _context.Update(reservation);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }


        /*
         [HttpGet]
         public IActionResult GetTablesDisponibles(DateTime date, TimeSpan heure)
         {
             var tablesReservees = _context.Reservations
                 .Where(r => r.Status == "confirm�e" && r.DateRes == date && r.Heure == heure)
                 .Select(r => r.IdTable)
                 .ToList();

             var tablesDisponibles = _context.TablesRestaurant
                 .Where(t => !tablesReservees.Contains(t.IdTable))
                 .Select(t => new
                 {
                     id = t.IdTable,
                     nom = $"Table {t.IdTable}"
                 })
                 .ToList();

             return Json(tablesDisponibles);
         }*/



    }
}
