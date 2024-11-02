﻿using EscuelaApp.Dominio.Interfaces;
using EscuelaApp.Persistencia.Data;
using Microsoft.AspNetCore.Mvc;

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
            return Ok(new { resultado = res });
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
            return Ok(new { resultado = await _repCourse.modificar(course) });
        }

        // DELETE api/<CourseController>/5
        [HttpDelete]
        [Route("BorrarCurso")]
        public async Task<ActionResult> BorrarCurso([FromBody] Course course)
        {
            return Ok(new { resultado = await _repCourse.eliminar(course) });
        }
    }
}