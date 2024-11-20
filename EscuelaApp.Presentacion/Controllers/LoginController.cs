using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EscuelaApp.Presentacion.Controllers
{
    public class LoginController : Controller
    {
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        // Método para autenticar el usuario
        public async Task<IActionResult> Authenticate(string username, string password)
        {
            // Credenciales quemadas
            string hcUsername = "admin";
            string hcPassword = "admin123";
            string hcFullName = "Admin Admin";
            string hcRole = "administrador";

            // Verficiar las credenciales
            if (username == hcUsername && password == hcPassword)
            {
                // Crear los claims
                var claims = new List<Claim> {
                    new Claim(ClaimTypes.Name, hcFullName),
                    new Claim(ClaimTypes.NameIdentifier, hcUsername),
                    new Claim(ClaimTypes.Role, hcRole),
                };

                // Crear la identidad del claim
                var claimIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                // Autenticar el usuario
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimIdentity));

                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "Nombre de usuario o contraseña incorrecto");
                return View("Index");
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            // Sign out the user
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index");
        }


    }
}