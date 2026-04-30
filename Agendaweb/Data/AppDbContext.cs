using Microsoft.EntityFrameworkCore;
using MiAgendaWeb.Models;

namespace MiAgendaWeb.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Contacto> Contactos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
    }
}