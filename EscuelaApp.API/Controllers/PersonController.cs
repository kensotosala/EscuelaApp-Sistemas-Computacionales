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

        // 1. Numero de estudiantes aprobados y reprobados.
        [HttpGet]
        [Route("ObtenerEstudiantesAprobadosyReprobados")]
        public async Task<ActionResult> ObtenerEstudiantesAprobadosyReprobados()
        {
            var res = await _repPerson.ObtenerEstudiantesAprobadosyReprobados();
            return Ok(new { resultado = new { Aprobados = res.Aprobados, Reprobados = res.Reprobados } });
        }

        // 2. Los datos de los estudiantes con las 3 mejores notas.
        [HttpGet]
        [Route("ObtenerTop3Notas")]
        public async Task<ActionResult> ObtenerTop3Notas()
        {
            var res = await _repPerson.ObtenerTop3Notas();
            return Ok(new { resultado = res });
        }

        // 3. Los datos de los estudiantes con las 3 notas más bajas.
        [HttpGet]
        [Route("ObtenerTop3NotasBajas")]
        public async Task<ActionResult> ObtenerTop3NotasBajas()
        {
            var res = await _repPerson.ObtenerTop3NotasBajas();
            return Ok(new { resultado = res });
        }

        // 5. Obtener el numero de docentes que tiene cada curso.
        [HttpGet]
        [Route("ObtenerDocentesPorCurso")]
        public async Task<ActionResult> ObtenerDocentesPorCurso()
        {
            var res = await _repPerson.ObtenerDocentesPorCurso();
            return Ok(new { resultado = res });
        }

        // 6. Obtener el promedio de notas de un estudiante entre todos sus cursos
        [HttpGet]
        [Route("ObtenerPromedioNotas")]
        public async Task<ActionResult> ObtenerPromedioNotas(int studenId)
        {
            var res = await _repPerson.ObtenerPromedioNotas(studenId);
            return Ok(new { resultado = res });
        }

        // 7. Obtener el numero de estudiantes inscritos en cada curso.
        [HttpGet]
        [Route("ObtenerEstudiantesPorCurso")]
        public async Task<ActionResult> ObtenerEstudiantesPorCurso()
        {
            var res = await _repPerson.ObtenerEstudiantesPorCurso();
            return Ok(new { resultado = res });
        }
    }
}