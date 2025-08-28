using DientesLimpios.Aplicacion.Utilidades.Mediador;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Aplicacion.CasoDeUso.Consultorios.Consultas.ObtenerConsultorio
{
    public class ConsultaObtenerDetalleConsultorio: IRequest<ConsultorioDetalleDTO>
    {
        public Guid Id { get; set; }
    }
}
