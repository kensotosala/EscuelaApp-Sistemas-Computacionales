﻿using EscuelaApp.Dominio.Interfaces;
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
            return _context.Courses.Include(c => c.Department).ToListAsync();
        }
    }
}