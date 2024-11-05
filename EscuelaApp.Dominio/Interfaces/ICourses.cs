using EscuelaApp.Persistencia.Data;

namespace EscuelaApp.Dominio.Interfaces
{
    public interface ICourses
    {
        // Insertar
        public Task<int> insertar(Course curso);

        // Modificar
        public Task<int> modificar(Course curso);

        // Eliminar
        public Task<int> eliminar(Course curso);

        // Listar
        public Task<List<Course>> obtenerTodo();

        // Consultar x ID
        public Task<Course?> obtenerCursosxId(int courseId);

        // Consultar x Nombre
        public Task<Course?> obtenerCursosxNombre(String courseName);

        public Task<int> getTotalCreditos();

        public Task<Dictionary<string, int>> getTotalxDepartment();

        public Task<Double> obtenerPromedioCreditos();
    }
}