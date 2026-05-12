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

        public async Task<List<Contacto>> ObtenerTodosAsync() => await _context.Contactos.ToListAsync();

        public async Task<Contacto> ObtenerPorIdAsync(int id) => await _context.Contactos.FindAsync(id);

        public async Task<(bool Success, string Message)> RegistrarContactoAsync(Contacto nuevo)
        {
            if (string.IsNullOrWhiteSpace(nuevo.Nombre) || string.IsNullOrWhiteSpace(nuevo.Telefono))
                return (false, "Nombre y Teléfono son obligatorios.");

            bool existe = await _context.Contactos.AnyAsync(c => c.Telefono == nuevo.Telefono || c.Correo == nuevo.Correo);

            if (existe)
                return (false, "Ya existe un contacto con ese teléfono o correo.");

            _context.Contactos.Add(nuevo);
            await _context.SaveChangesAsync();
            return (true, "¡Guardado!");
        }

        public async Task<(bool Success, string Message)> ActualizarContactoAsync(Contacto editado)
        {
            var db = await _context.Contactos.FindAsync(editado.Id);
            if (db == null) return (false, "No encontrado");

            bool duplicado = await _context.Contactos.AnyAsync(c =>
                (c.Telefono == editado.Telefono || c.Correo == editado.Correo) && c.Id != editado.Id);

            if (duplicado)
                return (false, "El teléfono o correo ya pertenecen a otro contacto.");

            db.Nombre = editado.Nombre;
            db.Apellido = editado.Apellido;
            db.Telefono = editado.Telefono;
            db.Correo = editado.Correo;
            db.EsFavorito = editado.EsFavorito;

            await _context.SaveChangesAsync();
            return (true, "Actualizado");
        }

        public async Task<bool> EliminarContactoAsync(int id)
        {
            var c = await _context.Contactos.FindAsync(id);
            if (c == null) return false;

            _context.Contactos.Remove(c);
            await _context.SaveChangesAsync();
            return true;
        }

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