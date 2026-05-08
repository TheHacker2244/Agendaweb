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

        // 1. Obtener lista completa
        public async Task<List<Contacto>> ObtenerTodosAsync() => await _context.Contactos.ToListAsync();

        // 2. Obtener un solo contacto por ID
        public async Task<Contacto> ObtenerPorIdAsync(int id) => await _context.Contactos.FindAsync(id);

        // 3. Crear nuevo contacto (Con validación de negocio)
        public async Task<(bool Success, string Message)> RegistrarContactoAsync(Contacto nuevo)
        {
            if (string.IsNullOrWhiteSpace(nuevo.Nombre) || string.IsNullOrWhiteSpace(nuevo.Telefono))
                return (false, "Nombre y Teléfono son obligatorios.");

            _context.Contactos.Add(nuevo);
            await _context.SaveChangesAsync();
            return (true, "¡Guardado!");
        }

        // 4. Actualizar contacto existente (Update)
        public async Task<(bool Success, string Message)> ActualizarContactoAsync(Contacto editado)
        {
            var db = await _context.Contactos.FindAsync(editado.Id);
            if (db == null) return (false, "No encontrado");

            db.Nombre = editado.Nombre;
            db.Apellido = editado.Apellido;
            db.Telefono = editado.Telefono;
            db.Correo = editado.Correo;
            db.EsFavorito = editado.EsFavorito; // Importante mantener el estado de favorito

            await _context.SaveChangesAsync();
            return (true, "Actualizado");
        }

        // 5. Eliminar contacto (Delete)
        public async Task<bool> EliminarContactoAsync(int id)
        {
            var c = await _context.Contactos.FindAsync(id);
            if (c == null) return false;

            _context.Contactos.Remove(c);
            await _context.SaveChangesAsync();
            return true;
        }

        // 6. Cambiar estado de favorito (El que faltaba dentro de la clase)
        public async Task CambiarEstadoFavoritoAsync(int id, bool estado)
        {
            var contacto = await _context.Contactos.FindAsync(id);
            if (contacto != null)
            {
                contacto.EsFavorito = estado;
                await _context.SaveChangesAsync();
            }
        }
    }
}