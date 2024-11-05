using EscuelaApp.Dominio.Interfaces;
using EscuelaApp.Persistencia.Data;
using Microsoft.EntityFrameworkCore;

namespace EscuelaApp.Persistencia.Repositorios
{
    public class RepositorioPerson : IPerson
    {
        private readonly SchoolDBContext _context;

        public RepositorioPerson(SchoolDBContext context)
        {
            _context = context;
        }

        public Task<List<Person>> ObtenerEstudiantesAprobadosXCurso(string courseName)
        {
            return _context.People
                .Include(p => p.StudentGrades)
                .Where(p => p.StudentGrades
                    .Any(c => c.Course.Title == courseName && c.Grade >= 3))
                .Distinct()
                .OrderBy(p => p.FirstName)
                .ThenBy(p => p.LastName)
                .ToListAsync();
        }

        public Task<List<Person>> obtenerEstudiantesAprobados()
        {
            return _context.People
                .Include(p => p.StudentGrades)
                .Where(p => p.StudentGrades.Any(g => g.Grade < 3))
                .Distinct()
                .OrderBy(p => p.FirstName)
                .ThenBy(p => p.LastName)
                .ToListAsync();
        }

        public async Task<(int Aprobados, int Reprobados)> ObtenerEstudiantesAprobadosyReprobados()
        {
            var totalCounts = await _context.People
                .Select(p => new
                {
                    Aprobados = p.StudentGrades.Count(g => g.Grade >= 3),
                    Reprobados = p.StudentGrades.Count(g => g.Grade < 3)
                })
                .ToListAsync();

            int totalAprobados = totalCounts.Sum(x => x.Aprobados);
            int totalReprobados = totalCounts.Sum(x => x.Reprobados);

            return (totalAprobados, totalReprobados);
        }

        // 2. Los datos de los estudiantes con las 3 notas más altas.
        public Task<List<Person>> ObtenerTop3Notas()
        {
            return _context.People
                .Include(p => p.StudentGrades)
                .OrderByDescending(p => p.StudentGrades.Max(g => g.Grade))
                .Take(3)
                .ToListAsync();
        }

        // 3. Los datos de los estudiantes con las 3 notas más bajas.
        public Task<List<Person>> ObtenerTop3NotasBajas()
        {
            return _context.People
                .Include(p => p.StudentGrades)
                .Where(p => p.StudentGrades.Any(g => g.Grade != null))
                .OrderBy(p => p.StudentGrades.Min(g => g.Grade))
                .Take(3)
                .ToListAsync();
        }

        // 5. Obtener el numero de docentes que tiene cada curso.
        public async Task<Dictionary<string, int>> ObtenerDocentesPorCurso()
        {
            var result = await _context.People
                .GroupBy(p => p.PersonId)
                .Select(group => new
                {
                    CourseID = group.Key,
                    CantDocentes = group.Count()
                })
                .ToListAsync();

            return result.ToDictionary(r => r.CourseID.ToString(), r => r.CantDocentes);
        }

        // 6. Obtener el promedio de notas de un estudiante entre todos sus cursos
        public async Task<double> ObtenerPromedioNotas(int studentId)
        {
            return (double)await _context.StudentGrades
                .Where(sg => sg.StudentId == studentId)
                .AverageAsync(sg => sg.Grade);
        }

        // 7. Obtener el numero de estudiantes inscritos en cada curso.
        public async Task<Dictionary<int, int>> ObtenerEstudiantesPorCurso()
        {
            return await _context.StudentGrades
                .GroupBy(sg => sg.CourseId)
                .ToDictionaryAsync(g => g.Key, g => g.Count());
        }
    }
}