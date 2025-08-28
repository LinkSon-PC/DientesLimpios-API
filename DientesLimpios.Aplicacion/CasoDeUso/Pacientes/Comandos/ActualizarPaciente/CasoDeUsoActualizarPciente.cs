using DientesLimpios.Aplicacion.Contratos.Persistencia;
using DientesLimpios.Aplicacion.Contratos.Repositorios;
using DientesLimpios.Aplicacion.Excepciones;
using DientesLimpios.Aplicacion.Utilidades.Mediador;
using DientesLimpios.Dominio.ObjetosDeValor;
using DientesLimpios.Persistencia.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Aplicacion.CasoDeUso.Pacientes.Comandos.ActualizarPaciente
{
    public class CasoDeUsoActualizarPciente : IRequestHandler<ComandoActualizarPaciente>
    {
        private readonly IRepositorioPacientes repositorio;
        private readonly IUnidadDeTrabajo unidadDeTrabajo;

        public CasoDeUsoActualizarPciente(IRepositorioPacientes repositorio, IUnidadDeTrabajo unidadDeTrabajo)
        {
            this.repositorio = repositorio;
            this.unidadDeTrabajo = unidadDeTrabajo;
        }

        public async Task Handle(ComandoActualizarPaciente request)
        {
            var paciente = await repositorio.ObtenerPorId(request.Id);
            if(paciente is null)
            {
                throw new ExcepcionNoEncontrado();
            }

            paciente.ActualizarNombre(request.Nombre);
            var email = new Email(request.Email);
            paciente.ActualizarEmail(email);

            try
            {
                await repositorio.Actualizar(paciente);
                await unidadDeTrabajo.Persistir();
            }
            catch (Exception ex)
            {
                await unidadDeTrabajo.Reversar();
                throw;
            }
        }
    }
}
