using EscuelaApp.Persistencia;
using EscuelaApp.Persistencia.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Llamar al método que inyecta nuestras dependencias
InyectarDependencias.ConfiguracionServicios(builder.Services);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Configurar la conexion string
builder.Services.AddDbContext<SchoolDBContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("SchoolDb"))
    );

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
