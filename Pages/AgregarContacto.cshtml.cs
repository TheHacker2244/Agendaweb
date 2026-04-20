using MiAgendaWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;

namespace MiAgendaWeb.Pages
{
    public class AgregarContactoModel : PageModel
    {
        public void OnGet() { }

        // Agregamos 'apellido' a los parámetros para que se guarde correctamente
        public IActionResult OnPost(string nombre, string apellido, string telefono, string correo)
        {
            if (!string.IsNullOrEmpty(nombre))
            {
                IndexModel.ListaContactos.Add(new Contacto
                {
                    Nombre = nombre,
                    Apellido = apellido, // Ahora sí guardamos el apellido
                    Telefono = telefono,
                    Correo = correo,
                    FechaRegistro = DateTime.Now
                });
            }
            return RedirectToPage("/Index");
        }
    }
}