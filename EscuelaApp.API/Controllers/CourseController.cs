using EscuelaApp.Dominio.DTO;
using EscuelaApp.Dominio.Interfaces;
using EscuelaApp.Persistencia.Data;
using Microsoft.AspNetCore.Authorization;
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
        //[Authorize(Roles = "administrador")]
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
        }

        // GET api/<CourseController>/5
        // GET api/obtenerxId/{id}
        [Authorize(Roles = "administrador")]
        [HttpGet]
        [Route("ObtenerxId")]
        public async Task<ActionResult> ObtenerxId(int id)
        {
            return Ok(new { resultado = await _repCourse.obtenerCursosxId(id) });
        }

        // POST api/<CourseController>
        //[Authorize(Roles = "administrador")]
        [HttpPost]
        [Route("GuardarCurso")]
        public async Task<ActionResult> GuardarCursoAsync([FromBody] Course course)
        {
            return Ok(new { resultado = await _repCourse.insertar(course) });
        }

        // PUT api/<CourseController>/5
        [Authorize(Roles = "administrador")]
        [HttpPut]
        [Route("ActualizarCurso")]
        public async Task<ActionResult> ActualizarCurso(int id, [FromBody] Course course)
        {
            var cursos = await _repCourse.modificar(course);

            return Ok(new { resultado = await _repCourse.modificar(course) });
        }

        // DELETE api/<CourseController>/5
        [Authorize(Roles = "administrador")]
        [HttpDelete]
        [Route("BorrarCurso")]
        public async Task<ActionResult> BorrarCurso([FromBody] Course course)
        {
            return Ok(new { resultado = await _repCourse.eliminar(course) });
        }

        // GET api/obtenerxId/{id}
        [HttpGet]
        [Authorize(Roles = "administrador")]
        [Route("ObtenerxNombre")]
        public async Task<ActionResult> ObtenerxNombre(string courseName)
        {
            return Ok(new { resultado = await _repCourse.obtenerCursosxNombre(courseName) });
        }

        [Authorize(Roles = "administrador")]
        [HttpGet]
        [Route("ObtenerTotalCreditos")]
        public async Task<ActionResult> ObtenerTotalCreditos()
        {
            return Ok(new { resultado = await _repCourse.getTotalCreditos() });
        }

        //[Authorize]
        [HttpGet]
        [Route("ObtenerTotalxDepartment")]
        public async Task<ActionResult> ObtenerTotalxDepartment()
        {
            return Ok(new { resultado = await _repCourse.getTotalxDepartment() });
        }

        // 4. Obtener el promedio de créditos de los cursos.
        [Authorize(Roles = "administrador")]
        [HttpGet]
        [Route("ObtenerPromedioCreditos")]
        public async Task<ActionResult> ObtenerPromedioCreditos()
        {
            return Ok(new { resultado = await _repCourse.obtenerPromedioCreditos() });
        }
    }
}