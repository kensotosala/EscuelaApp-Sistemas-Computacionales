using EscuelaApp.Dominio.DTO;
using EscuelaApp.Dominio.Interfaces;
using EscuelaApp.Persistencia.Data;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EscuelaApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourses _repCourse;

        public CourseController(ICourses repCourse)
        {
            _repCourse = repCourse;
        }

        // GET: api/obtenerTodo
        [HttpGet]
        [Route("ObtenerTodo")]
        public async Task<ActionResult> ObtenerTodo()
        {
            var res = await _repCourse.obtenerTodo();

            var cursosDTO = res.Select(c => new CourseDTO
            {
                CourseID = c.CourseId,
                Title = c.Title,
                Credits = c.Credits,
                DepartmentId = c.DepartmentId,
                NombreDepartamento = c.Department?.Name
            }).ToList();

            var jsonRes = JsonConvert.SerializeObject(cursosDTO);
            return Content(jsonRes, "application/json");

            // return Ok(new { resultado = res });
        }

        // GET api/<CourseController>/5
        // GET api/obtenerxId/{id}
        [HttpGet]
        [Route("ObtenerxId")]
        public async Task<ActionResult> ObtenerxId(int id)
        {
            return Ok(new { resultado = await _repCourse.obtenerCursosxId(id) });
        }

        // POST api/<CourseController>
        [HttpPost]
        [Route("GuardarCurso")]
        public async Task<ActionResult> GuardarCursoAsync([FromBody] Course course)
        {
            return Ok(new { resultado = await _repCourse.insertar(course) });
        }

        // PUT api/<CourseController>/5
        [HttpPut]
        [Route("ActualizarCurso")]
        public async Task<ActionResult> ActualizarCurso(int id, [FromBody] Course course)
        {
            var cursos = await _repCourse.modificar(course);

            return Ok(new { resultado = await _repCourse.modificar(course) });
        }

        // DELETE api/<CourseController>/5
        [HttpDelete]
        [Route("BorrarCurso")]
        public async Task<ActionResult> BorrarCurso([FromBody] Course course)
        {
            return Ok(new { resultado = await _repCourse.eliminar(course) });
        }

        // GET api/obtenerxId/{id}
        [HttpGet]
        [Route("ObtenerxNombre")]
        public async Task<ActionResult> ObtenerxNombre(string courseName)
        {
            return Ok(new { resultado = await _repCourse.obtenerCursosxNombre(courseName) });
        }

        [HttpGet]
        [Route("ObtenerTotalCreditos")]
        public async Task<ActionResult> ObtenerTotalCreditos()
        {
            return Ok(new { resultado = await _repCourse.getTotalCreditos() });
        }

        [HttpGet]
        [Route("ObtenerTotalxDepartment")]
        public async Task<ActionResult> ObtenerTotalxDepartment()
        {
            return Ok(new { resultado = await _repCourse.getTotalxDepartment() });
        }

        // 4. Obtener el promedio de créditos de los cursos.
        [HttpGet]
        [Route("ObtenerPromedioCreditos")]
        public async Task<ActionResult> ObtenerPromedioCreditos()
        {
            return Ok(new { resultado = await _repCourse.obtenerPromedioCreditos() });
        }
    }
}