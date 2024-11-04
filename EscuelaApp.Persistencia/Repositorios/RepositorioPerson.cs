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
    }
}