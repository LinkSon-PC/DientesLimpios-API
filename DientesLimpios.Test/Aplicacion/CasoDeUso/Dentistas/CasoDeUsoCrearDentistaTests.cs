using DientesLimpios.Aplicacion.CasoDeUso.Dentistas.Comandos.CrearDentista;
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

namespace DientesLimpios.Test.Aplicacion.CasoDeUso.Dentistas
{
    [TestClass]
    public class CasoDeUsoCrearDentistaTests
    {
#pragma warning disable CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de agregar el modificador "required" o declararlo como un valor que acepta valores NULL.
        private IRepositorioDentistas repositorio;
        private IUnidadDeTrabajo unidadDeTrabajo;
        private CasoDeUsoCrearDentista casoDeUso;
#pragma warning restore CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de agregar el modificador "required" o declararlo como un valor que acepta valores NULL.

        [TestInitialize]
        public void Setup()
        {
            repositorio = Substitute.For<IRepositorioDentistas>();
            unidadDeTrabajo = Substitute.For<IUnidadDeTrabajo>();
            casoDeUso = new CasoDeUsoCrearDentista(repositorio, unidadDeTrabajo);
        }

        [TestMethod]
        public async Task Handlle_CuandoDatosValidos_CrearDentistaYPersisteYRetornaId()
        {
            var comando = new ComandoCrearDentista { Nombre = "Dentista A", Email = "dentistaA@ejemplo.com" };
            var dentista = new Dentista(comando.Nombre, new Email(comando.Email));
            var id = dentista.Id;
            
            repositorio.Agregar(Arg.Any<Dentista>()).Returns(Task.FromResult(dentista));

            var respuesta = await casoDeUso.Handle(comando);

            Assert.AreEqual(id, respuesta);
            await repositorio.Received(1).Agregar(Arg.Any<Dentista>());
            await unidadDeTrabajo.Received(1).Persistir();
        }

        [TestMethod]
        public async Task Handle_CuandoOcurreExcepcion_ReversarYLanzaExcepcion()
        {
            var comando = new ComandoCrearDentista { Nombre = "Paciente A", Email = "pacienteA@ejemplo.com" };
            repositorio.Agregar(Arg.Any<Dentista>())
                .Throws(new InvalidOperationException("Error al insertar"));

            await Assert.ThrowsExceptionAsync<InvalidOperationException>(() => casoDeUso.Handle(comando));

            await unidadDeTrabajo.Received(1).Reversar();
            await unidadDeTrabajo.DidNotReceive().Persistir();
        }
    }
}
