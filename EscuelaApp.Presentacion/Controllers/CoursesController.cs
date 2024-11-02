using EscuelaApp.Dominio.Interfaces;
using EscuelaApp.Persistencia.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EscuelaApp.Presentacion.Controllers
{
    public class CoursesController : Controller
    {
        private readonly SchoolDBContext _context;
        private readonly ICourses _repCourse;
        private readonly IDepartments _repDepartment;

        public CoursesController(SchoolDBContext context, ICourses repCourse, IDepartments repDepartment)
        {
            _context = context;
            _repCourse = repCourse;
            _repDepartment = repDepartment;
        }

        // GET: Courses
        public async Task<IActionResult> Index()
        {
            return View(await _repCourse.obtenerTodo());
        }

        // GET: Courses/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var course = await _repCourse.obtenerCursosxId(id);

            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // GET: Courses/Create
        public async Task<IActionResult> Create()
        {
            ViewData["DepartmentId"] = new SelectList(await _repDepartment.obtenerTodo(), "DepartmentId", "Name");
            return View();
        }

        // POST: Courses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CourseId,Title,Credits,DepartmentId")] Course course)
        {
            int res = 0;
            if (ModelState.IsValid)
            {
                res = await _repCourse.insertar(course);

                if (res == 1)
                {
                    // TempData["Mensaje"] = "Curso Insertado Correctamente";
                    ViewBag.Mensaje = "Curso Insertado Correctamente";
                    ViewBag.TipoMsg = "alert-primary";
                }
                else if (res == 3)
                {
                    ViewBag.Mensaje = $"Error al insertar. Ya existe un curso con el ID: {course.CourseId}";
                    ViewBag.TipoMsg = "alert-warning";
                }
                else
                {
                    // TempData["Mensaje"] = "Curso no ha sido guardado";
                    ViewBag.Mensaje = "Curso no ha sido guardado";
                    ViewBag.TipoMsg = "alert-danger";
                }
                // return RedirectToAction(nameof(Index));
                return View("Index", await _repCourse.obtenerTodo());
            }
            ViewData["DepartmentId"] = new SelectList(await _repDepartment.obtenerTodo(), "DepartmentId", "Name", course.DepartmentId);
            return View(course);
        }

        // GET: Courses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound(); // Handle case where id is null
            }

            int courseID = (int)id;
            var course = await _repCourse.obtenerCursosxId(courseID);

            if (course == null)
            {
                return NotFound(); // Handle case where course is not found
            }

            ViewData["DepartmentId"] = new SelectList(await _repDepartment.obtenerTodo(), "DepartmentId", "Name", course.DepartmentId);
            return View(course);
        }

        // POST: Courses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CourseId,Title,Credits,DepartmentId")] Course course)
        {
            if (id != course.CourseId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                int res = await _repCourse.modificar(course);
                if (res == 1)
                {
                    // TempData["Mensaje"] = "Curso Insertado Correctamente";
                    ViewBag.Mensaje = "Curso Modificado Correctamente";
                    ViewBag.TipoMsg = "alert-primary";
                }
                else
                {
                    // TempData["Mensaje"] = "Curso no ha sido guardado";
                    ViewBag.Mensaje = "Curso no ha sido modificado";
                    ViewBag.TipoMsg = "alert-danger";
                }
                // return RedirectToAction(nameof(Index));
                return View("Index", await _repCourse.obtenerTodo());
            }
            ViewData["DepartmentId"] = new SelectList(await _repDepartment.obtenerTodo(), "DepartmentId", "Name", course.DepartmentId);
            return View(course);
        }

        // GET: Courses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _repCourse.obtenerCursosxId((int)id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var course = await _repCourse.obtenerCursosxId(id);

            if (course != null)
            {
                int res = await _repCourse.eliminar(course);
                if (res == 1)
                {
                    // TempData["Mensaje"] = "Curso Insertado Correctamente";
                    ViewBag.Mensaje = "Curso eliminado correctamente";
                    ViewBag.TipoMsg = "alert-primary";
                }
                else
                {
                    // TempData["Mensaje"] = "Curso no ha sido guardado";
                    ViewBag.Mensaje = "Curso no ha sido eliminado";
                    ViewBag.TipoMsg = "alert-danger";
                }
                // return RedirectToAction(nameof(Index));
                return View("Index", await _repCourse.obtenerTodo());
            }

            return RedirectToAction(nameof(Index));
        }
    }
}