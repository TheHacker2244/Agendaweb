using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore; // Necesario para .ToListAsync()
using MiAgendaWeb.Data; // Donde está tu AppDbContext
using MiAgendaWeb.Models; // Donde están tus modelos

namespace MiAgendaWeb.Pages
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _context;

        // El constructor recibe la conexión de base de datos automáticamente
        public IndexModel(AppDbContext context)
        {
            _context = context;
        }

        // Esta lista ahora se llenará con datos reales de SQL Server
        public List<Contacto> ListaContactos { get; set; } = new();

        public async Task OnGetAsync()
        {
            // Consultamos la tabla Contactos en SQL Server
            ListaContactos = await _context.Contactos.ToListAsync();
        }
    }
}