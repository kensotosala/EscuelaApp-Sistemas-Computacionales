using EscuelaApp.Dominio.Interfaces;
using EscuelaApp.Persistencia.Repositorios;
using Microsoft.Extensions.DependencyInjection;

namespace EscuelaApp.Persistencia
{
    public class InyectarDependencias
    {
        public static void ConfiguracionServicios(IServiceCollection servicios)
        {
            servicios.AddScoped<ICourses, RepositorioCourses>();
            servicios.AddScoped<IDepartments, RepositorioDepartments>();
            servicios.AddScoped<IPerson, RepositorioPerson>();
        }
    }
}