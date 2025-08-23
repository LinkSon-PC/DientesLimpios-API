using DientesLimpios.Dominio.Excepciones;
using DientesLimpios.Dominio.ObjetosDeValor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Test.Dominio.ObjetosDeValor
{
    [TestClass]
    public class IntervaloDeTiempoTest
    {
        [TestMethod]
        [ExpectedException(typeof(ExcepcionDeReglaDeNegocio))]
        public void Constructor_FechaIncioPosteriorAFechaFin_LanzaExcepcion()
        {
            new IntervaloDeTiempo(DateTime.UtcNow, DateTime.UtcNow.AddDays(-1));
        }

        [TestMethod]
        public void Constructor_ParametrosCorrectos_NoLanzaExcepcion()
        {
            new IntervaloDeTiempo(DateTime.UtcNow, DateTime.UtcNow.AddMinutes(30));
        }
    }
}
