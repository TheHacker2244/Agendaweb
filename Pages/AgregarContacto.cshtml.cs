using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MiAgendaWeb.Models;
using MiAgendaWeb.Services; // Importante para usar tu nueva capa
using System.Threading.Tasks;

namespace MiAgendaWeb.Pages

{
    public class AgregarContactoModel : PageModel
    {
        private readonly ContactoService _contactoService;

        // Ahora inyectamos el SERVICIO, no el Contexto directamente
        public AgregarContactoModel(ContactoService contactoService)
        {
            _contactoService = contactoService;
        }

        [BindProperty]
        public Contacto NuevoContacto { get; set; }

        public void OnGet() { }

        public async Task<IActionResult> OnPostAsync()
        {
            // 1. Verifica validaciones básicas (como el formato de email)
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // 2. Llama a la lógica de negocio para validar duplicados
            var resultado = await _contactoService.RegistrarContactoAsync(NuevoContacto);

            if (!resultado.Success)
            {
                // Si el servicio dice que hay un error (ej: duplicado), lo mostramos
                ModelState.AddModelError(string.Empty, resultado.Message);
                return Page();
            }

            // Si todo salió bien, volvemos al inicio
            return RedirectToPage("/Index");
        }
    }
}