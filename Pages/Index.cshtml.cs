using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MiAgendaWeb.Models;

namespace MiAgendaWeb.Pages
{
    public class IndexModel : PageModel
    {
        // Esta lista es compartida por toda la app
        public static List<Contacto> ListaContactos { get; set; } = new List<Contacto>();

        public void OnGet() { }
    }
}