using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MiAgendaWeb.Data;
using MiAgendaWeb.Models;

namespace MiAgendaWeb.Pages.Home
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _context;

        public IndexModel(AppDbContext context)
        {
            _context = context;
        }

        public IList<Contacto> Contactos { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var usuario = HttpContext.Session.GetString("Usuario");
            if (string.IsNullOrEmpty(usuario))
            {
                return RedirectToPage("/Login/Index");
            }

            if (!string.IsNullOrEmpty(SearchTerm))
            {
                return RedirectToPage("/Index", new { searchTerm = SearchTerm });
            }

            Contactos = await _context.Contactos.ToListAsync();
            return Page();
        }
    }
}