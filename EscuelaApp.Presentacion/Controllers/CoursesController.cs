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
            if (ModelState.IsValid)
            {
                int res = await _repCourse.insertar(course);
                return RedirectToAction(nameof(Index));
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
                return RedirectToAction(nameof(Index));
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
            }

            return RedirectToAction(nameof(Index));
        }
    }
}