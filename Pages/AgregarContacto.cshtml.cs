using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MiAgendaWeb.Data; // Tu conexión
using MiAgendaWeb.Models; // Tus modelos
using System;
using System.Threading.Tasks;

namespace MiAgendaWeb.Pages
{
    public class AgregarContactoModel : PageModel
    {
        private readonly AppDbContext _context;

        // Inyectamos el contexto de la base de datos
        public AgregarContactoModel(AppDbContext context)
        {
            _context = context;
        }

        public void OnGet() { }

        // Cambiamos a Task<IActionResult> para que sea asíncrono y eficiente
        public async Task<IActionResult> OnPostAsync(string nombre, string apellido, string telefono, string correo)
        {
            if (!string.IsNullOrEmpty(nombre))
            {
                // Creamos el nuevo objeto contacto
                var nuevoContacto = new Contacto
                {
                    Nombre = nombre,
                    // Asegúrate de que tu modelo 'Contacto' tenga la propiedad Apellido
                    // Si no la tiene, puedes comentarla o agregarla al modelo
                    Telefono = telefono,
                    Correo = correo,
                    // FechaRegistro = DateTime.Now // Úsalo si tu tabla tiene este campo
                };

                // Guardamos en SQL Server
                _context.Contactos.Add(nuevoContacto);
                await _context.SaveChangesAsync();
            }

            // Al terminar, regresamos a la lista principal
            return RedirectToPage("/Index");
        }
    }
}