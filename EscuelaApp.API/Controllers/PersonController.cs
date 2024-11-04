using EscuelaApp.Dominio.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EscuelaApp.API.Controllers
{
    public class PersonController : Controller
    {
        private readonly IPerson _repPerson;

        public PersonController(IPerson repPerson)
        {
            _repPerson = repPerson;
        }

        // GET: PersonController
        public ActionResult Index()
        {
            return View();
        }

        // GET: PersonController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PersonController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PersonController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PersonController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PersonController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PersonController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PersonController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        [Route("ObtenerEstudiantesAprobados")]
        public async Task<ActionResult> ObtenerEstudiantesAprobados()
        {
            var res = await _repPerson.obtenerEstudiantesAprobados();
            return Ok(new { resultado = res });
        }

        [HttpGet]
        [Route("ObtenerEstudiantesAprobadosXCurso")]
        public async Task<ActionResult> ObtenerEstudiantesAprobadosXCurso(String courseName)
        {
            var res = await _repPerson.ObtenerEstudiantesAprobadosXCurso(courseName);
            return Ok(new { resultado = res });
        }
    }
}