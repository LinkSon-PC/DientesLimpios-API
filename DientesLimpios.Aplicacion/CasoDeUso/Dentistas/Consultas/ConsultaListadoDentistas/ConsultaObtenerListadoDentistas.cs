using DientesLimpios.Aplicacion.CasoDeUso.Pacientes.Consultas.ObtenerListadoPacientes;
using DientesLimpios.Aplicacion.Utilidades.Comunes;
using DientesLimpios.Aplicacion.Utilidades.Mediador;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Aplicacion.CasoDeUso.Dentistas.Consultas.ConsultaListadoDentistas
{
    public class ConsultaObtenerListadoDentistas : FiltroDentistasDTO, IRequest<PaginadoDTO<DentistaListadoDTO>>
    {

    }
}
