using DientesLimpios.Aplicacion.CasoDeUso.Pacientes.Consultas.ObtenerListadoPacientes;
using DientesLimpios.Dominio.Entidades;
using DientesLimpios.Dominio.ObjetosDeValor;
using DientesLimpios.Persistencia.Repositorios;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Test.Aplicacion.CasoDeUso.Pacientes
{
    [TestClass]
    public class CasoDeUsoObtenerListadoPacientesTests
    {
#pragma warning disable CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de agregar el modificador "required" o declararlo como un valor que acepta valores NULL.
        private IRepositorioPacientes repositorio;
        private CasoDeUsoObtenerListadoPacientes casoDeUso;
#pragma warning restore CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de agregar el modificador "required" o declararlo como un valor que acepta valores NULL.

        [TestInitialize]
        public void Setup()
        {
            repositorio = Substitute.For<IRepositorioPacientes>();
            casoDeUso = new CasoDeUsoObtenerListadoPacientes(repositorio);
        }

        [TestMethod]
        public async Task Handle_RetornaPacientesPaginadosCorrectamente()
        {
            var pagina = 1;
            var registrosPorPagina = 2;

            var filtroPacienteDTO = new FiltroPacienteDTO { Pagina = pagina, RegistrosPorPagina = registrosPorPagina };

            var paciente1 = new Paciente("Paciente A", new Email("pacienteA@ejemplo.com"));
            var paciente2 = new Paciente("Paciente B", new Email("pacienteB@ejemplo.com"));

            IEnumerable<Paciente> pacientes = new List<Paciente> { paciente1, paciente2 };

            repositorio.ObtenerFiltrado(Arg.Any<FiltroPacienteDTO>()).Returns(Task.FromResult(pacientes));

            repositorio.ObtenerCantidadTotalRegistros().Returns(Task.FromResult(10));

            var request = new ConsultaObtenerListadoPacientes
            {
                Pagina = pagina,
                RegistrosPorPagina = registrosPorPagina
            };

            var resultado = await casoDeUso.Handle(request);

            Assert.AreEqual(10, resultado.Total);
            Assert.AreEqual(2, resultado.Elementos.Count);
            Assert.AreEqual("Paciente A", resultado.Elementos[0].Nombre.ToString());
            Assert.AreEqual("Paciente B", resultado.Elementos[1].Nombre.ToString());
        }

        [TestMethod]
        public async Task Handle_CuandoNoHayPacientes_RetornaListaVaciaYTotalCero()
        {
            var pagina = 1;
            var registrosPorPagina = 1;

            var filtroPacienteDTO = new FiltroPacienteDTO { Pagina = pagina, RegistrosPorPagina = registrosPorPagina };

            IEnumerable<Paciente> pacientes = new List<Paciente>();

            repositorio.ObtenerFiltrado(Arg.Any<FiltroPacienteDTO>()).Returns(Task.FromResult(pacientes));

            repositorio.ObtenerCantidadTotalRegistros().Returns(Task.FromResult(0));

            var request = new ConsultaObtenerListadoPacientes
            {
                Pagina = pagina,
                RegistrosPorPagina = registrosPorPagina
            };

            var resultado = await casoDeUso.Handle(request);

            Assert.AreEqual(0, resultado.Total);
            Assert.IsNotNull(resultado.Elementos);
            Assert.AreEqual(0, resultado.Elementos.Count);
        }
    }
}
