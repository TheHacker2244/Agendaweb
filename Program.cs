using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore; // Necesario para usar UseSqlServer
using System.IO;
using MiAgendaWeb.Data; 

var builder = WebApplication.CreateBuilder(new WebApplicationOptions
{
    Args = args,
    ContentRootPath = Directory.GetCurrentDirectory(),
    WebRootPath = "wwwroot"
});

// --- SECCIÓN DE CONEXIÓN A BASE DE DATOS ---
// Aquí le decimos a la app que use la cadena de conexión que tenemos en el archivo JSON
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
// --------------------------------------------

builder.Services.AddRazorPages();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles(); // Activa CSS, imágenes y JS de wwwroot

app.UseRouting();
app.UseAuthorization();
app.MapRazorPages();

app.Run();