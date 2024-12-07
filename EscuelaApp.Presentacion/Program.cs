using EscuelaApp.Persistencia;
using EscuelaApp.Persistencia.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

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
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme; // Ensure this line is here
})
.AddCookie(options =>
{
    options.LoginPath = "/Login"; // Specify your login path
    options.LogoutPath = "/Login/Logout"; // Specify your logout path
    options.ExpireTimeSpan = TimeSpan.FromMinutes(30); // Cookie lifetime
});

// Configure Swagger with cookie-based authentication
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Your API", Version = "v1" });

    c.AddSecurityDefinition("Cookie", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.ApiKey,
        In = ParameterLocation.Cookie,
        Name = ".AspNetCore.Cookies",
        Description = "Authentication Cookie"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Cookie"
                }
            },
            new string[] { }
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();  // Authentication should come first
app.UseAuthorization();  // Authorization follows authentication

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
