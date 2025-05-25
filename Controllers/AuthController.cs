using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

[Route("auth")]
public class AuthController : Controller
{
    private readonly ApplicationDbContext _context;

    public AuthController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet("login")]
    public IActionResult Login()
    {
        return View();
    }
    [HttpPost("login")]
    public async Task<IActionResult> Login(string email, string password)
    {
        var user = await _context.Utilisateurs
            .FirstOrDefaultAsync(u => u.Email == email && u.MotDePasse == password);

        if (user == null)
        {
            ModelState.AddModelError("", "Email or password incorrect.");
            return View();
        }

        var claims = new List<Claim>
    {
        new Claim(ClaimTypes.NameIdentifier, user.IdUtilisateur.ToString()),
        new Claim(ClaimTypes.Name, user.Nom + " " + user.Prenom),
        new Claim(ClaimTypes.Email, user.Email),
        new Claim(ClaimTypes.Role, user.Role) // <<--- Ajout du r�le ici
    };

        var identity = new ClaimsIdentity(claims, "MyCookieAuth");
        var principal = new ClaimsPrincipal(identity);

        await HttpContext.SignInAsync("MyCookieAuth", principal);

        return RedirectToAction("Index", "Home");
    }


  

    [HttpGet("register")]
public IActionResult Register()
{
    ViewBag.Restaurants = _context.Restaurants.ToList();
    return View();
}

[HttpPost("register")]
public async Task<IActionResult> Register(
    string nom, 
    string prenom, 
    string email, 
    string telephone, 
    string password, 
    string role,
    int? idRestaurant,
    string tablesAssignees)
{
    var exists = await _context.Utilisateurs.AnyAsync(u => u.Email == email);
    if (exists)
    {
        ModelState.AddModelError("", "Email déjà enregistré.");
        return View();
    }

    // Validation pour les rôles employé
    var employeeRoles = new[] { "Admin", "Serveur", "Hotesse" };
    if (employeeRoles.Contains(role))
    {
        if (!idRestaurant.HasValue)
        {
            ModelState.AddModelError("", "Un restaurant doit être sélectionné pour ce rôle.");
            return View();
        }
        
        var restaurantExists = await _context.Restaurants.AnyAsync(r => r.IdRestaurant == idRestaurant);
        if (!restaurantExists)
        {
            ModelState.AddModelError("", "Restaurant invalide.");
            return View();
        }
    }

    // Création utilisateur
    var user = new Utilisateur
    {
        Nom = nom,
        Prenom = prenom,
        Email = email,
        Telephone = telephone,
        MotDePasse = password, // À hasher en production!
        Role = role
    };

    _context.Utilisateurs.Add(user);
    await _context.SaveChangesAsync();

    // Création employé si nécessaire
    if (employeeRoles.Contains(role))
    {
        var employe = new Employe
        {
            Nom = nom,
            Prenom = prenom,
            Role = role,
            IdRestaurant = idRestaurant.Value,
            TablesAssignees = tablesAssignees
        };

        _context.Employes.Add(employe);
        await _context.SaveChangesAsync();
    }

    return RedirectToAction("Login");
}


    [Authorize]
    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync("MyCookieAuth");
        return RedirectToAction("Login");
    }
    [HttpGet("accessdenied")]
    public IActionResult AccessDenied()
    {
        return View();
    }

}
