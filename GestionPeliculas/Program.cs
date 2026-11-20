using Microsoft.EntityFrameworkCore;
using GestionPeliculas.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

// 1. Instanciamos tu clase Conexion y obtenemos el string de conexión
var conexion = new Conexion();
var connectionString = conexion.ObtenerCadenaConexion();

// 2. Registramos AppDbContext con Pomelo MySQL
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

var app = builder.Build();

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