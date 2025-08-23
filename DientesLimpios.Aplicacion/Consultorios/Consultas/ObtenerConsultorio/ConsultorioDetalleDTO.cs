using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Aplicacion.Consultorios.Consultas.ObtenerConsultorio
{
    public class ConsultorioDetalleDTO
    {
        public Guid Id { get; set; }
        public required string Nombre { get; set; }
    }
}
