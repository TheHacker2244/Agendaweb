using MiAgendaWeb.Data;
using MiAgendaWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace MiAgendaWeb.Pages.Favoritos
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _context;

        public IndexModel(AppDbContext context)
        {
            _context = context;
        }

        public List<Contacto> MisFavoritos { get; set; } = new();

        public async Task<IActionResult> OnGetAsync()
        {
            var usuarioSesion = HttpContext.Session.GetString("Usuario");
            if (string.IsNullOrEmpty(usuarioSesion))
            {
                return RedirectToPage("/Login/Index");
            }

            // FILTRO CRÍTICO: Solo favoritos
            MisFavoritos = await _context.Contactos
                .Where(c => c.EsFavorito == true)
                .ToListAsync();

            return Page();
        }

        // Método para "quitar" de favoritos desde esta misma pantalla
        public async Task<IActionResult> OnPostQuitarFavoritoAsync(int id)
        {
            var contacto = await _context.Contactos.FindAsync(id);
            if (contacto != null)
            {
                contacto.EsFavorito = false; // Lo apagamos
                await _context.SaveChangesAsync();
            }
            return RedirectToPage();
        }
    }
}