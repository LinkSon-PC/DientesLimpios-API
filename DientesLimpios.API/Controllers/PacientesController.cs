using DientesLimpios.API.DTOs.Pacientes;
using DientesLimpios.API.Utilidades;
using DientesLimpios.Aplicacion.CasoDeUso.Consultorios.Comandos.BorrarConsultorio;
using DientesLimpios.Aplicacion.CasoDeUso.Pacientes.Comandos.ActualizarPaciente;
using DientesLimpios.Aplicacion.CasoDeUso.Pacientes.Comandos.CrearPaciente;
using DientesLimpios.Aplicacion.CasoDeUso.Pacientes.Consultas.ObtenerDetallePaciente;
using DientesLimpios.Aplicacion.CasoDeUso.Pacientes.Consultas.ObtenerListadoPacientes;
using DientesLimpios.Aplicacion.Utilidades.Mediador;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DientesLimpios.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PacientesController : ControllerBase
    {
        private readonly IMediator mediador;

        public PacientesController(IMediator mediador)
        {
            this.mediador = mediador;
        }

        [HttpGet]
        public async Task<ActionResult<List<PacienteListadoDTO>>> Get(
            [FromQuery] ConsultaObtenerListadoPacientes consulta
            ) 
        {
            var resultado = await mediador.Send(consulta);
            HttpContext.InsertarPaginacionnEnCabecera(resultado.Total);
            return resultado.Elementos;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PacienteDetalleDTO>> Get(Guid id)
        {
            var consulta = new ConsultaObtenerDetallePaciente() { Id = id };
            var resultado = await mediador.Send(consulta);
            return resultado;
        }

        [HttpPost]
        public async Task<IActionResult> Post(CrearPacienteDTO crearPacienteDTO)
        {
            var comando = new ComandoCrearPaciente { Nombre = crearPacienteDTO.Nombre, Email = crearPacienteDTO.Email };
            await mediador.Send(comando);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, ActualizarPacienteDTO actualizarPacienteDTO)
        {
            var comando = new ComandoActualizarPaciente {
                Id = id,
                Nombre = actualizarPacienteDTO.Nombre, 
                Email = actualizarPacienteDTO.Email 
            };
            await mediador.Send(comando);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var comando = new ComandoBorrarConsultorio { Id = id};
            await mediador.Send(comando);
            return Ok();
        }
    }
}
