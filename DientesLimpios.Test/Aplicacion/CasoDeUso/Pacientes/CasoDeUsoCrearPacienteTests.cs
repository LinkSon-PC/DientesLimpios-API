using DientesLimpios.Aplicacion.CasoDeUso.Pacientes.Comandos.CrearPaciente;
using DientesLimpios.Aplicacion.Contratos.Persistencia;
using DientesLimpios.Aplicacion.Contratos.Repositorios;
using DientesLimpios.Dominio.Entidades;
using DientesLimpios.Dominio.ObjetosDeValor;
using DientesLimpios.Persistencia.Repositorios;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Test.Aplicacion.CasoDeUso.Pacientes
{
    [TestClass]
    public class CasoDeUsoCrearPacienteTests
    {
        #pragma warning disable CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de agregar el modificador "required" o declararlo como un valor que acepta valores NULL.
        private IRepositorioPacientes repositorio;
        private IUnidadDeTrabajo unidadDeTrabajo;
        private CasoDeUsoCrearPaciente casoDeUso;
        #pragma warning restore CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de agregar el modificador "required" o declararlo como un valor que acepta valores NULL.

        [TestInitialize]
        public void Setup()
        {
            repositorio = Substitute.For<IRepositorioPacientes>();
            unidadDeTrabajo = Substitute.For<IUnidadDeTrabajo>();
            casoDeUso = new CasoDeUsoCrearPaciente(repositorio, unidadDeTrabajo);
        }

        [TestMethod]
        public async Task Handlle_CuandoDatosValidos_CrearPacienteYPersisteYRetornaId()
        {
            var comando = new ComandoCrearPaciente { Nombre = "Paciente A", Email = "pacienteA@ejemplo.com" };
            var pacienteCreado = new Paciente(comando.Nombre, new Email(comando.Email));
            var id = pacienteCreado.Id;

            repositorio.Agregar(Arg.Any<Paciente>()).Returns(pacienteCreado);

            var idResultado = await casoDeUso.Handle(comando);

            Assert.AreEqual(id, idResultado);
            await repositorio.Received(1).Agregar(Arg.Any<Paciente>());
            await unidadDeTrabajo.Received(1).Persistir();
        }

        [TestMethod]
        public async Task Handle_CuandoOcurreExcepcion_ReversarYLanzaExcepcion()
        {
            var comando = new ComandoCrearPaciente { Nombre = "Paciente A", Email = "pacienteA@ejemplo.com" };
            repositorio.Agregar(Arg.Any<Paciente>())
                .Throws(new InvalidOperationException("Error al insertar"));

            await Assert.ThrowsExceptionAsync<InvalidOperationException>(() => casoDeUso.Handle(comando));

            await unidadDeTrabajo.Received(1).Reversar();
            await unidadDeTrabajo.DidNotReceive().Persistir();
        }
    }
}
