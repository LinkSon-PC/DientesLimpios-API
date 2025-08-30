using DientesLimpios.Aplicacion.Utilidades.Mediador;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Aplicacion.CasoDeUso.Dentistas.Consultas.ObtenerDetallePaciente
{
    public class ConsultaObtenerDetalleDentista : IRequest<DentistaDetalleDTO>
    {
        public required Guid Id { get; set; }
    }
}
