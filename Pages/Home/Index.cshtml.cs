using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MiAgendaWeb.Data;
using MiAgendaWeb.Models;

namespace MiAgendaWeb.Pages.Home
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _context; // Cambiado a AppDbContext

        public IndexModel(AppDbContext context)
        {
            _context = context;
        }

        public IList<Contacto> Contactos { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            // Seguridad de sesión
            var usuario = HttpContext.Session.GetString("Usuario");
            if (string.IsNullOrEmpty(usuario))
            {
                return RedirectToPage("/Login/Index");
            }

            // Carga real de la base de datos
            Contactos = await _context.Contactos.ToListAsync();
            return Page();
        }
    }
}