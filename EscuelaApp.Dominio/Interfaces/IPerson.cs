using EscuelaApp.Persistencia.Data;

namespace EscuelaApp.Dominio.Interfaces
{
    public interface IPerson
    {
        // Obtener estudiantes desaprobados
        public Task<List<Person>> obtenerEstudiantesAprobados();
        public Task<List<Person>> ObtenerEstudiantesAprobadosXCurso(string courseName);

    }
}