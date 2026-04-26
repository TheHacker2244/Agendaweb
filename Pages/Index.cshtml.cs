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

        public async Task<IActionResult> OnGetAsync()
        {
            // VERIFICACIÓN DE SEGURIDAD
            var usuarioSesion = HttpContext.Session.GetString("Usuario");

            if (string.IsNullOrEmpty(usuarioSesion))
            {
                return RedirectToPage("/Login/Index");
            }

            ListaContactos = await _context.Contactos.ToListAsync();
            return Page();
        }
    }
}