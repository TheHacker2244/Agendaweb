using MiAgendaWeb.Data;
using MiAgendaWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace MiAgendaWeb.Pages
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _context;

        public IndexModel(AppDbContext context)
        {
            _context = context;
        }

        public List<Contacto> ListaContactos { get; set; } = new();

        // Captura el término de búsqueda desde la URL
        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            // VERIFICACIÓN DE SEGURIDAD
            var usuarioSesion = HttpContext.Session.GetString("Usuario");

            if (string.IsNullOrEmpty(usuarioSesion))
            {
                return RedirectToPage("/Login/Index");
            }

            // Lógica de búsqueda avanzada
            var query = _context.Contactos.AsQueryable();

            if (!string.IsNullOrEmpty(SearchTerm))
            {
                query = query.Where(c => c.Nombre.Contains(SearchTerm) ||
                                         c.Apellido.Contains(SearchTerm) ||
                                         c.Correo.Contains(SearchTerm));
            }

            ListaContactos = await query.ToListAsync();
            return Page();
        }
    }
}