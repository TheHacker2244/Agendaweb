using MiAgendaWeb.Models;
using MiAgendaWeb.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MiAgendaWeb.Pages.Favoritos
{
    public class IndexModel : PageModel
    {
        private readonly ContactoService _contactoService;

        public IndexModel(ContactoService contactoService)
        {
            _contactoService = contactoService;
        }

        public List<Contacto> MisFavoritos { get; set; } = new();

        public async Task<IActionResult> OnGetAsync()
        {
            // Verificación de sesión opcional si quieres proteger la página
            var usuarioSesion = HttpContext.Session.GetString("Usuario");
            if (string.IsNullOrEmpty(usuarioSesion)) return RedirectToPage("/Login/Index");

            var todos = await _contactoService.ObtenerTodosAsync();
            MisFavoritos = todos.Where(c => c.EsFavorito).ToList();
            return Page();
        }

        public async Task<IActionResult> OnPostQuitarFavoritoAsync(int id)
        {
            await _contactoService.CambiarEstadoFavoritoAsync(id, false);
            return RedirectToPage();
        }

    }
}