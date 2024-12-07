using EscuelaApp.Presentacion.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace EscuelaApp.Presentacion.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [Authorize]
        public IActionResult Index()
        {
            ViewBag.Nombre = User.Identity?.Name;
            ViewBag.Role = User.FindFirst(ClaimTypes.Role)?.Value;
            ViewBag.ImagenUsuario = User.FindFirst("ImagenUsuario")?.Value;
            return View();
        }

        [Authorize]
        [Authorize(Roles = "administrador")]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}