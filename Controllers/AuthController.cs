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
            new Claim(ClaimTypes.Email, user.Email)
        };

        var identity = new ClaimsIdentity(claims, "MyCookieAuth");
        var principal = new ClaimsPrincipal(identity);

        await HttpContext.SignInAsync("MyCookieAuth", principal);

        return RedirectToAction("Index", "Home");
    }

    [HttpGet("register")]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(string nom, string prenom, string email, string telephone, string password)
    {
        var exists = await _context.Utilisateurs.AnyAsync(u => u.Email == email);
        if (exists)
        {
            ModelState.AddModelError("", "Email already registered.");
            return View();
        }

        var user = new Utilisateur
        {
            Nom = nom,
            Prenom = prenom,
            Email = email,
            Telephone = telephone,
            MotDePasse = password, // directly storing password
            ProgrammeFidelite = 0
        };

        _context.Utilisateurs.Add(user);
        await _context.SaveChangesAsync();

        return RedirectToAction("Login");
    }

    [Authorize]
    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync("MyCookieAuth");
        return RedirectToAction("Login");
    }
}
