using EscuelaApp.Persistencia.Data;

namespace EscuelaApp.Dominio.Interfaces
{
    public interface IPerson
    {
        // Obtener estudiantes desaprobados
        public Task<List<Person>> obtenerEstudiantesAprobados();

        public Task<List<Person>> ObtenerEstudiantesAprobadosXCurso(string courseName);

        public Task<(int Aprobados, int Reprobados)> ObtenerEstudiantesAprobadosyReprobados();

        public Task<List<Person>> ObtenerTop3Notas();

        public Task<List<Person>> ObtenerTop3NotasBajas();

        public Task<Dictionary<string, int>> ObtenerDocentesPorCurso();

        public Task<Double> ObtenerPromedioNotas(int studenId);

        public Task<Dictionary<int, int>> ObtenerEstudiantesPorCurso();
    }
}