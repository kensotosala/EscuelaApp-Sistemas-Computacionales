using EscuelaApp.Dominio.Interfaces;
using EscuelaApp.Persistencia.Data;
using Microsoft.EntityFrameworkCore;

namespace EscuelaApp.Persistencia.Repositorios
{
    public class RepositorioCourses : ICourses
    {
        private readonly SchoolDBContext _context;

        public RepositorioCourses(SchoolDBContext context)
        {
            _context = context;
        }

        public Task<int> eliminar(Course curso)
        {
            _context.Courses.Remove(curso);
            return _context.SaveChangesAsync();
        }

        public async Task<int> insertar(Course curso)
        {
            //var c = obtenerCursosxId(curso.CourseId);
            //if (curso == null)
            //{
            _context.Add(curso);
            var res = _context.SaveChangesAsync();
            return await res;
            //}
            //else
            //{
            //    return await Task.FromResult(3);
            //}
        }

        public Task<int> modificar(Course curso)
        {
            _context.Update(curso);
            return _context.SaveChangesAsync();
        }

        public Task<Course?> obtenerCursosxId(int courseId)
        {
            return _context.Courses.Include(c => c.Department)
                .FirstOrDefaultAsync(m => m.CourseId == courseId);
        }

        public Task<List<Course>> obtenerTodo()
        {
            return _context.Courses.Include(c => c.Department).OrderBy(c => c.Title).ThenBy(c => c.Credits).ToListAsync();

            // .ThenByDescending()
            // OrderByDescending(c=>c.Title)
            // .Take(1) == TOP en SQL
        }

        // Buscar por nombre de Curso
        public Task<Course?> obtenerCursosxNombre(string courseName)
        {
            return _context.Courses.Include(c => c.Department)
                .FirstOrDefaultAsync(m => m.Title == courseName);
        }

        public Task<int> getTotalCreditos()
        {
            return _context.Courses.SumAsync(c => c.Credits);
        }

        public async Task<Dictionary<string, int>> getTotalxDepartment()
        {
            var result = await _context.Courses
                .GroupBy(c => c.Department)
                .Select(group => new
                {
                    DepartmentName = group.Key != null ? group.Key.Name : "Desconocido",
                    TotalCreditos = group.Sum(c => c.Credits)
                })
                .ToListAsync();

            return result.ToDictionary(r => r.DepartmentName, r => r.TotalCreditos);
        }
    }
}