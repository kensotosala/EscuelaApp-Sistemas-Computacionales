using EscuelaApp.Dominio.Interfaces;
using EscuelaApp.Persistencia.Data;
using Microsoft.EntityFrameworkCore;

namespace EscuelaApp.Persistencia.Repositorios
{
    public class RepositorioDepartments : IDepartments
    {
        private readonly SchoolDBContext _context;

        public RepositorioDepartments(SchoolDBContext context)
        {
            _context = context;
        }

        public Task<int> eliminar(Department departamento)
        {
            _context.Departments.Remove(departamento);
            return _context.SaveChangesAsync();
        }

        public Task<int> insertar(Department departamento)
        {
            _context.Add(departamento);
            return _context.SaveChangesAsync();
        }

        public Task<int> modificar(Department departamento)
        {
            _context.Update(departamento);
            return _context.SaveChangesAsync();
        }

        public Task<List<Department>> obtenerTodo()
        {
            return _context.Departments.ToListAsync();
        }

        public Task<Department?> obtenerxId(int DepartmentId)
        {
            return _context.Departments.FirstOrDefaultAsync(d => d.DepartmentId == DepartmentId);
        }
    }
}