using EscuelaApp.Persistencia;
using EscuelaApp.Persistencia.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Llamar al método que inyecta nuestras dependencias
InyectarDependencias.ConfiguracionServicios(builder.Services);

builder.Services.AddAuthorization();

// Inyectar httpclient
builder.Services.AddHttpClient();

// Add services to the container.
builder.Services.AddControllersWithViews();

// Configurar la conexion string
builder.Services.AddDbContext<SchoolDBContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("SchoolDb"))
    );

// Agregar el servicio de autenticación por cookies
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
{
    options.LoginPath = "/Login";
    options.LogoutPath = "/Login/Logout";
    options.AccessDeniedPath = "/Home/Index";
    options.ExpireTimeSpan = TimeSpan.FromMinutes(30); // Vida de la cookie
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();