using DientesLimpios.Aplicacion.Consultorios.Consultas.ObtenerListadoConsultorios;
using DientesLimpios.Aplicacion.Contratos.Repositorios;
using DientesLimpios.Dominio.Entidades;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Test.Aplicacion.CasoDeUso.Consultorios
{
    [TestClass]
    public class CasoDeUsoObtenerListadoConsultoriosTests
    {
        private IRepositorioConsultorios repositorio;
        private CasoDeUsoObtenerListadoConsultorios casoDeUso;

        [TestInitialize]
        public void Setup() 
        {
            repositorio = Substitute.For<IRepositorioConsultorios>();
            casoDeUso = new CasoDeUsoObtenerListadoConsultorios(repositorio);
        }

        [TestMethod]
        public async Task Handle_CuandoHayConsultorios_RetornaListaDeConsultoriosListadoDTO()
        {
            var consultorios = new List<Consultorio>
            {
                new Consultorio("Consultorio A"),
                new Consultorio("Consultorio B")
            };

            repositorio.ObtenerTodos().Returns(consultorios);

            var esperando = consultorios.Select(c =>
                new ConsultorioListadoDTO { Id = c.Id, Nombre = c.Nombre }).ToList();

            var resultado = await casoDeUso.Handle(new ConsultaObtenerListadoConsultorios());

            Assert.AreEqual(esperando.Count, resultado.Count);

            for (int i = 0; i < esperando.Count; i++)
            {
                Assert.AreEqual(esperando[i].Id, resultado[i].Id);
                Assert.AreEqual(esperando[i].Nombre, resultado[i].Nombre);
            }
        }

        [TestMethod]
        public async Task Handle_CuandoNoHayConsultorios_RetornaListaVacia()
        {
            repositorio.ObtenerTodos().Returns(new List<Consultorio>());

            var resultado = await casoDeUso.Handle(new ConsultaObtenerListadoConsultorios());

            Assert.IsNotNull(resultado);
            Assert.AreEqual(0, resultado.Count);
        }
    }
}
