using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MiAgendaWeb.Models;
using MiAgendaWeb.Services;

namespace MiAgendaWeb.Pages.EditarContactos
{
    public class EditarContactoModel : PageModel
    {
        private readonly ContactoService _contactoService;
        public EditarContactoModel(ContactoService contactoService) => _contactoService = contactoService;

        public List<Contacto> ListaContactos { get; set; } = new();

        [BindProperty]
        public Contacto ContactoSeleccionado { get; set; }

        public async Task OnGetAsync(int? id)
        {
            // Cargamos la lista completa para la columna izquierda
            ListaContactos = await _contactoService.ObtenerTodosAsync();

            // Si hay un ID en la URL, cargamos el detalle para la derecha
            if (id.HasValue) // Corregido: HasValue con H mayúscula
            {
                ContactoSeleccionado = await _contactoService.ObtenerPorIdAsync(id.Value);
            }
        }

        // Para actualizar los datos cuando des clic al botón morado
        public async Task<IActionResult> OnPostAsync()
        {
            // Si hay un error de validación, debemos volver a cargar la lista de la izquierda
            // antes de devolver la página, si no, la columna izquierda desaparecerá.
            if (!ModelState.IsValid)
            {
                ListaContactos = await _contactoService.ObtenerTodosAsync();
                return Page();
            }

            // Guardar cambios en la DB
            var resultado = await _contactoService.ActualizarContactoAsync(ContactoSeleccionado);

            // Redirigir para limpiar el estado del formulario y ver los cambios frescos
            return RedirectToPage(new { id = ContactoSeleccionado.Id });
        }

        public async Task<IActionResult> OnPostEliminarAsync(int id)
        {
            await _contactoService.EliminarContactoAsync(id);
            return RedirectToPage("/EditarContactos/EditarContacto", new { id = (int?)null });
        }

        public async Task<IActionResult> OnPostFavoritoAsync(int id, bool estado)
        {
            await _contactoService.CambiarEstadoFavoritoAsync(id, estado);
            return RedirectToPage(new { id = id });
        }
    }
}