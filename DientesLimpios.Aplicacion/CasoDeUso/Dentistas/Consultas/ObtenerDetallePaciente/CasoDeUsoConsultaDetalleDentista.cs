using DientesLimpios.Aplicacion.Contratos.Persistencia;
using DientesLimpios.Aplicacion.Contratos.Repositorios;
using DientesLimpios.Aplicacion.Excepciones;
using DientesLimpios.Aplicacion.Utilidades.Mediador;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Aplicacion.CasoDeUso.Dentistas.Consultas.ObtenerDetallePaciente
{
    public class CasoDeUsoConsultaDetalleDentista : IRequestHandler<ConsultaObtenerDetalleDentista, DentistaDetalleDTO>
    {
        private readonly IRepositorioDentistas repositorio;
        private readonly IUnidadDeTrabajo unidadDeTrabajo;

        public CasoDeUsoConsultaDetalleDentista(IRepositorioDentistas repositorio, IUnidadDeTrabajo unidadDeTrabajo)
        {
            this.repositorio = repositorio;
            this.unidadDeTrabajo = unidadDeTrabajo;
        }

        public async Task<DentistaDetalleDTO> Handle(ConsultaObtenerDetalleDentista request)
        {
            var dentista = await repositorio.ObtenerPorId(request.Id);
            if (dentista == null) 
            {
                throw new ExcepcionNoEncontrado();
            }
            return dentista.ADto();
        }
    }
}
