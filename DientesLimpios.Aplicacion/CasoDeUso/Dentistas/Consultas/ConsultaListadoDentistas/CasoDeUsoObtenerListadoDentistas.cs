using DientesLimpios.Aplicacion.Contratos.Persistencia;
using DientesLimpios.Aplicacion.Contratos.Repositorios;
using DientesLimpios.Aplicacion.Utilidades.Comunes;
using DientesLimpios.Aplicacion.Utilidades.Mediador;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Aplicacion.CasoDeUso.Dentistas.Consultas.ConsultaListadoDentistas
{
    public class CasoDeUsoObtenerListadoDentistas : IRequestHandler<ConsultaObtenerListadoDentistas, PaginadoDTO<DentistaListadoDTO>>
    {
        private readonly IRepositorioDentistas repositorio;

        public CasoDeUsoObtenerListadoDentistas(IRepositorioDentistas repositorio)
        {
            this.repositorio = repositorio;
        }

        public async Task<PaginadoDTO<DentistaListadoDTO>> Handle(ConsultaObtenerListadoDentistas request)
        {
            var dentistas = await repositorio.ObtenerFiltrado(request);
            var total = await repositorio.ObtenerCantidadTotalRegistros();
            var dentistasDTO = dentistas.Select(d => d.ADto()).ToList();

            var paginadoDTO = new PaginadoDTO<DentistaListadoDTO>
            {
                Total = total,
                Elementos = dentistasDTO
            };
            return paginadoDTO;
        }
    }
}
