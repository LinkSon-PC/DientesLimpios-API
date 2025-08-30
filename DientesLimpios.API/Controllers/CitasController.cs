using DientesLimpios.API.DTOs.Citas;
using DientesLimpios.Aplicacion.CasoDeUso.Citas.Comandos.CancelarCita;
using DientesLimpios.Aplicacion.CasoDeUso.Citas.Comandos.CompletarCita;
using DientesLimpios.Aplicacion.CasoDeUso.Citas.Comandos.CrearCita;
using DientesLimpios.Aplicacion.CasoDeUso.Citas.Consultas.ObtenerDetalleCita;
using DientesLimpios.Aplicacion.CasoDeUso.Citas.Consultas.ObtenerListadoCitas;
using DientesLimpios.Aplicacion.Utilidades.Mediador;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DientesLimpios.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitasController : ControllerBase
    {
        private readonly IMediator mediator;

        public CitasController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<CitaListadoDTO>>> Get([FromQuery] ConsultaObteenrListadoCitas consulta)
        {
            var resultado = await mediator.Send(consulta);
            return resultado;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CitaDetalleDTO>> Get(Guid id)
        {
            var consulta = new ConsultaObtenerDetalleCita { Id = id };
            var resultado = await mediator.Send(consulta);
            return resultado;
        }

        [HttpPost]
        public async Task<ActionResult> Post(CrearCitaDTO crearCitaDTO)
        {
            var cita = new ComandoCrearCita
            {
                ConsultorioId = crearCitaDTO.ConsultorioId,
                DentistaId = crearCitaDTO.DentistaId,
                PacienteId = crearCitaDTO.PacienteId,
                FechaInicio = crearCitaDTO.FechaInicio,
                FechaFin = crearCitaDTO.FechaFin
            };

            var resultado = await mediator.Send(cita);
            return Ok();
        }

        [HttpPost("{id}/completar")]
        public async Task<ActionResult> Completar(Guid id)
        {
            var comando = new ComandoCompletarCita { Id = id };
            await mediator.Send(comando);
            return Ok();
        }

        [HttpPost("{id}/cancelar")]
        public async Task<ActionResult> Cancelar(Guid id)
        {
            var comando = new ComandoCancelarCita { Id = id };
            await mediator.Send(comando);
            return Ok();
        }
    }
}
