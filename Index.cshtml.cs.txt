using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MiAgendaWeb.Models;

namespace MiAgendaWeb.Pages
{
    public class IndexModel : PageModel
    {
        // Lista estática para que los datos no se borren al navegar
        public static List<Contacto> ListaContactos { get; set; } = new List<Contacto>();

        public void OnGet() { }

        public IActionResult OnPost(string nombre, string telefono, string correo)
        {
            if (!string.IsNullOrEmpty(nombre))
            {
                ListaContactos.Add(new Contacto
                {
                    Nombre = nombre,
                    Telefono = telefono,
                    Correo = correo
                });
            }
            return RedirectToPage();
        }
    }
}