using MiAgendaWeb.Data;
using MiAgendaWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace MiAgendaWeb.Services
{
    public class ContactoService
    {
        private readonly AppDbContext _context;

        public ContactoService(AppDbContext context)
        {
            _context = context;
        }

        // Método para obtener todos los contactos
        public async Task<List<Contacto>> ObtenerTodosAsync()
        {
            return await _context.Contactos.ToListAsync();
        }

        // Método con LÓGICA DE NEGOCIO para registrar
        public async Task<(bool Success, string Message)> RegistrarContactoAsync(Contacto nuevo)
        {
            // 1. Validación: No permitir campos vacíos
            if (string.IsNullOrWhiteSpace(nuevo.Nombre) || string.IsNullOrWhiteSpace(nuevo.Apellido) || string.IsNullOrWhiteSpace(nuevo.Correo))
            {
                return (false, "Regla de Negocio: Todos los campos son obligatorios.");
            }

            // 2. Validación: Formato de correo electrónico
            if (!nuevo.Correo.Contains("@") || !nuevo.Correo.Contains("."))
            {
                return (false, "Regla de Negocio: El formato del correo no es válido.");
            }

            // 3. Validación: Evitar registros duplicados
            var existe = await _context.Contactos.AnyAsync(c => c.Correo == nuevo.Correo);
            if (existe)
            {
                return (false, "Regla de Negocio: Ya existe un contacto con este correo electrónico.");
            }

            // 4. Validación: Coherencia de datos (Ejemplo: Teléfono no negativo/vacío)
            if (string.IsNullOrWhiteSpace(nuevo.Telefono))
            {
                return (false, "Regla de Negocio: El número de teléfono es necesario.");
            }

            // Si pasa todas las reglas, guardamos en la base de datos
            _context.Contactos.Add(nuevo);
            await _context.SaveChangesAsync();
            return (true, "¡Contacto guardado exitosamente!");
        }
    }
}