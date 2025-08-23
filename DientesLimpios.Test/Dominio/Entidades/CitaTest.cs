using DientesLimpios.Dominio.Entidades;
using DientesLimpios.Dominio.Enum;
using DientesLimpios.Dominio.Excepciones;
using DientesLimpios.Dominio.ObjetosDeValor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Test.Dominio.Entidades
{
    [TestClass]
    public class CitaTest
    {
        private Guid _pacienteId = Guid.NewGuid();
        private Guid _dentistaId = Guid.NewGuid();
        private Guid _consultorioId = Guid.NewGuid();
        private IntervaloDeTiempo _intervalo = new IntervaloDeTiempo(DateTime.UtcNow.AddDays(1),
            DateTime.UtcNow.AddDays(2));

        [TestMethod]
        public void Constructor_CitaValida_EstadoEsProgramada()
        {
            var cita = new Cita(_pacienteId, _dentistaId, _consultorioId, _intervalo);

            Assert.AreEqual(_pacienteId, cita.PacienteId);
            Assert.AreEqual(_dentistaId, cita.DentistaId);
            Assert.AreEqual(_consultorioId , cita.ConsultorioId);
            Assert.AreEqual(EstadoCita.Programada, cita.Estado);
            Assert.AreNotEqual(Guid.Empty, cita.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(ExcepcionDeReglaDeNegocio))]
        public void Constructor_FechaInicioEnElPasado_LanzaExcepcion()
        {
            var intervalo = new IntervaloDeTiempo(DateTime.UtcNow.AddDays(-1), DateTime.UtcNow);
            new Cita(_pacienteId, _dentistaId, _consultorioId, intervalo);
        }

        [TestMethod]
        public void Cancelar_CitaProgramada_CambiaEstadoACancelada()
        {
            var cita = new Cita(_pacienteId, _dentistaId, _consultorioId, _intervalo);
            cita.Cancelar();
            Assert.AreEqual(EstadoCita.Cancelada, cita.Estado);
        }

        [TestMethod]
        [ExpectedException(typeof(ExcepcionDeReglaDeNegocio))]
        public void Cancelar_CitaProgramada_LanzaExcepcion()
        {
            var cita = new Cita(_pacienteId, _dentistaId, _consultorioId, _intervalo);
            cita.Cancelar();
            cita.Cancelar();
        }

        [TestMethod]
        public void Completar_CitaProgramada_CambiaEstadoACompletada()
        {
            var cita = new Cita(_pacienteId, _dentistaId, _consultorioId, _intervalo);
            cita.Completar();
            Assert.AreEqual(EstadoCita.Completada, cita.Estado);
        }

        [TestMethod]
        [ExpectedException(typeof(ExcepcionDeReglaDeNegocio))]
        public void Completar_CitaProgramada_LanzaExcepcion()
        {
            var cita = new Cita(_pacienteId, _dentistaId, _consultorioId, _intervalo);
            cita.Cancelar();
            cita.Completar();
        }
    }
}
