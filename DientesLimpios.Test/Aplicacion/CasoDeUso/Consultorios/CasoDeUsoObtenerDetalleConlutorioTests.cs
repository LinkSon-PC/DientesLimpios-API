using DientesLimpios.Aplicacion.Consultorios.Consultas.ObtenerConsultorio;
using DientesLimpios.Aplicacion.Contratos.Repositorios;
using DientesLimpios.Aplicacion.Excepciones;
using DientesLimpios.Dominio.Entidades;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Test.Aplicacion.CasoDeUso.Consultorios
{
    [TestClass]
    public class CasoDeUsoObtenerDetalleConlutorioTests
    {
#pragma warning disable CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de agregar el modificador "required" o declararlo como un valor que acepta valores NULL.
        private IRepositorioConsultorios repositorio;        
        private CasoDeUsoObtenerDetalleConsultorio casoDeUso;
#pragma warning restore CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de agregar el modificador "required" o declararlo como un valor que acepta valores NULL.

        [TestInitialize]
        public void Setup()
        {
            repositorio = Substitute.For<IRepositorioConsultorios>();
            casoDeUso = new CasoDeUsoObtenerDetalleConsultorio(repositorio);
        }

        [TestMethod]
        public async Task Handle_ConsultorioExiste_RetornaDTO()
        {
            // Preparacion
            var consultorio = new Consultorio("Consultorio A");
            var id = consultorio.Id;
            var consulta = new ConsultaObtenerDetalleConsultorio { Id = id };

            repositorio.ObtenerPorId(id).Returns(consultorio);

            // Prueba
            var resultado = await casoDeUso.Handle(consulta);

            // Verificacion
            Assert.IsNotNull(resultado);
            Assert.AreEqual(id, resultado.Id);
            Assert.AreEqual("Consultorio A", resultado.Nombre);
        }

        [TestMethod]
        [ExpectedException(typeof(ExcepcionNoEncontrado))]
        public async Task Handle_ConsultorioObtenerDetalleConsultorio()
        {
            // Preparacion
            var id = Guid.NewGuid();
            var consulta = new ConsultaObtenerDetalleConsultorio { Id = id };

            repositorio.ObtenerPorId(id).ReturnsNull();

            // Prueba
            await casoDeUso.Handle(consulta);
        }
    }
}
