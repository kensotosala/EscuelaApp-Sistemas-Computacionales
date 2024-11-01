using EscuelaApp.Persistencia.Data;

namespace EscuelaApp.Dominio.Interfaces
{
    public interface IDepartments
    {
        // Insertar
        public Task<int> insertar(Department departamento);

        // Modificar
        public Task<int> modificar(Department departamento);

        // Eliminar
        public Task<int> eliminar(Department departamento);

        // Listar
        public Task<List<Department>> obtenerTodo();

        // Consultar x ID
        public Task<Department?> obtenerxId(int DepartmentId);
    }
}