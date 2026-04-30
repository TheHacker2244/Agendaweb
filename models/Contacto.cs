using System.ComponentModel.DataAnnotations;

namespace MiAgendaWeb.Models
{
    public class Contacto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string Nombre { get; set; } = "";

        [Required(ErrorMessage = "El apellido es obligatorio")]
        public string Apellido { get; set; } = "";

        [Required(ErrorMessage = "El teléfono es obligatorio")]
        public string Telefono { get; set; } = "";

        [Required(ErrorMessage = "El correo es obligatorio")]
        [EmailAddress(ErrorMessage = "El formato de correo no es válido")]
        public string Correo { get; set; } = "";

        public DateTime FechaRegistro { get; set; } = DateTime.Now;
    }
}