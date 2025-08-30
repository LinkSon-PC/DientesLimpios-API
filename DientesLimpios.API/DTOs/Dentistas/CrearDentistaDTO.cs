using System.ComponentModel.DataAnnotations;

namespace DientesLimpios.API.DTOs.Dentistas
{
    public class CrearDentistaDTO
    {
        [Required]
        [MaxLength(250)]
        public required string Nombre { get; set; }
        [Required]
        [MaxLength(264)]
        [EmailAddress]
        public required string Email { get; set; }
    }
}
