using DientesLimpios.Aplicacion.CasoDeUso.Dentistas.Consultas.ConsultaListadoDentistas;
using DientesLimpios.Aplicacion.CasoDeUso.Pacientes.Consultas.ObtenerListadoPacientes;
using DientesLimpios.Aplicacion.Contratos.Repositorios;
using DientesLimpios.Dominio.Entidades;
using DientesLimpios.Dominio.ObjetosDeValor;
using NSubstitute;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Test.Aplicacion.CasoDeUso.Dentistas
{
    [TestClass]
    public class CasoDeUsoObtenerListadoPacienetsTests
    {
#pragma warning disable CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de agregar el modificador "required" o declararlo como un valor que acepta valores NULL.
        private IRepositorioDentistas repositorio;
        private CasoDeUsoObtenerListadoDentistas casoDeUso;
#pragma warning restore CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de agregar el modificador "required" o declararlo como un valor que acepta valores NULL.

        [TestInitialize]
        public void Setup()
        {
            repositorio = Substitute.For<IRepositorioDentistas>();
            casoDeUso = new CasoDeUsoObtenerListadoDentistas(repositorio);
        }

        [TestMethod]
        public async Task Handle_RetornaPacientesPaginadosCorrectamente()
        {
            var pagina = 1;
            var registrosPorPagina = 2;
            var dentista1 = new Dentista("Dentista A", new Email("denstaA@ejemplo.com"));
            var dentista2 = new Dentista("Dentista B", new Email("denstaB@ejemplo.com"));

            IEnumerable<Dentista> dentistas = new List<Dentista> { dentista1, dentista2 };

            repositorio.ObtenerFiltrado(Arg.Any<FiltroDentistasDTO>()).Returns(Task.FromResult(dentistas));
            
            repositorio.ObtenerCantidadTotalRegistros().Returns(10);

            var consulta = new ConsultaObtenerListadoDentistas
            {
                Pagina = pagina,
                RegistrosPorPagina = registrosPorPagina
            };

            var respuesta = await casoDeUso.Handle(consulta);

            Assert.AreEqual(10, respuesta.Total);
            Assert.AreEqual(2, respuesta.Elementos.Count);
            Assert.AreEqual("Dentista A", respuesta.Elementos[0].Nombre);
            Assert.AreEqual("Dentista B", respuesta.Elementos[1].Nombre);
        }

        [TestMethod]
        public async Task Handle_CuandoNoHayDentistas_RetornaListaVaciaYTotalCero()
        {
            var pagina = 1;
            var registrosPorPagina = 1;

            var filtroPacienteDTO = new FiltroPacienteDTO { Pagina = pagina, RegistrosPorPagina = registrosPorPagina };

            IEnumerable<Dentista> dentistas = new List<Dentista>();

            repositorio.ObtenerFiltrado(Arg.Any<FiltroDentistasDTO>()).Returns(Task.FromResult(dentistas));

            repositorio.ObtenerCantidadTotalRegistros().Returns(Task.FromResult(0));

            var request = new ConsultaObtenerListadoDentistas
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
