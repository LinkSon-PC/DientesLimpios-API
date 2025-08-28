using System.ComponentModel.DataAnnotations;

namespace DientesLimpios.API.DTOs
{
    public class ActualizarConsultorioDTO
    {
        [Required]
        [StringLength(150)]
        public required string Nombre { get; set; }
    }
}
