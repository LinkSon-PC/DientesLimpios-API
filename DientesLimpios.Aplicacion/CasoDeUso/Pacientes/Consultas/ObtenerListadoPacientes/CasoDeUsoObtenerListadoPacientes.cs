using DientesLimpios.Aplicacion.Utilidades.Comunes;
using DientesLimpios.Aplicacion.Utilidades.Mediador;
using DientesLimpios.Persistencia.Repositorios;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Aplicacion.CasoDeUso.Pacientes.Consultas.ObtenerListadoPacientes
{
    public class CasoDeUsoObtenerListadoPacientes : IRequestHandler<ConsultaObtenerListadoPacientes, PaginadoDTO<PacienteListadoDTO>>
    {
        private readonly IRepositorioPacientes repositorio;

        public CasoDeUsoObtenerListadoPacientes(IRepositorioPacientes repositorio)
        {
            this.repositorio = repositorio;
        }

        public async Task<PaginadoDTO<PacienteListadoDTO>> Handle(ConsultaObtenerListadoPacientes request)
        {
            var pacientes = await repositorio.ObtenerFiltrado(request);
            var totalPacientes = await repositorio.ObtenerCantidadTotalRegistros();
            var pacientesDTO = pacientes.Select(paciente => paciente.ADto()).ToList();

            var paginadoDTO = new PaginadoDTO<PacienteListadoDTO>
            {
                Elementos = pacientesDTO,
                Total = totalPacientes
            };

            return paginadoDTO;
        }
    }
}
