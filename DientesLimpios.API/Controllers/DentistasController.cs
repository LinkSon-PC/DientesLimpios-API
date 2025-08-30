using DientesLimpios.API.DTOs.Dentistas;
using DientesLimpios.API.DTOs.Pacientes;
using DientesLimpios.API.Utilidades;
using DientesLimpios.Aplicacion.CasoDeUso.Dentistas.Comandos.ActualizarDentista;
using DientesLimpios.Aplicacion.CasoDeUso.Dentistas.Comandos.BorrarDentista;
using DientesLimpios.Aplicacion.CasoDeUso.Dentistas.Comandos.CrearDentista;
using DientesLimpios.Aplicacion.CasoDeUso.Dentistas.Consultas.ConsultaListadoDentistas;
using DientesLimpios.Aplicacion.CasoDeUso.Dentistas.Consultas.ObtenerDetallePaciente;
using DientesLimpios.Aplicacion.CasoDeUso.Pacientes.Comandos.ActualizarPaciente;
using DientesLimpios.Aplicacion.Utilidades.Mediador;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DientesLimpios.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DentistasController : ControllerBase
    {
        private readonly IMediator mediator;

        public DentistasController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DentistaDetalleDTO>> Get(Guid id)
        {
            var consulta = new ConsultaObtenerDetalleDentista { Id = id };
            var resultado = await mediator.Send(consulta);
            return resultado;
        }

        [HttpGet]
        public async Task<ActionResult<List<DentistaListadoDTO>>> Get([FromQuery] ConsultaObtenerListadoDentistas filtro)
        {
            var resultado = await mediator.Send(filtro);
            HttpContext.InsertarPaginacionnEnCabecera(resultado.Total);
            return resultado.Elementos;
        }

        [HttpPost]
        public async Task<IActionResult> Post(CrearDentistaDTO dentista)
        {
            var comando = new ComandoCrearDentista { Nombre = dentista.Nombre, Email = dentista.Email };
            var resultado = await mediator.Send(comando);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, ActualizarDentistaDTO dentista)
        {
            var comando = new ComandoActualizarDentista
            {
                Id = id,
                Nombre = dentista.Nombre,
                Email = dentista.Email
            };
            await mediator.Send(comando);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var comando = new ComandoBorrarDentista { Id = id };
            await mediator.Send(comando);
            return Ok();
        }
    }
}
