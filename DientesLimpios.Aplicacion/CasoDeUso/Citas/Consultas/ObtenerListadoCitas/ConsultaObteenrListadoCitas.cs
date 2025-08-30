using DientesLimpios.Aplicacion.Utilidades.Mediador;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Aplicacion.CasoDeUso.Citas.Consultas.ObtenerListadoCitas
{
    public class ConsultaObteenrListadoCitas : FiltroCitasDTO, IRequest<List<CitaListadoDTO>>
    {
    }
}
