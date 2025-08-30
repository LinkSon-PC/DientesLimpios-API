using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Aplicacion.CasoDeUso.Dentistas.Consultas.ConsultaListadoDentistas
{
    public class FiltroDentistasDTO
    {
        public int Pagina { get; set; } = 1;
        public int RegistrosPorPagina { get; set; } = 10;
        public string? Nombre { get; set; }
        public string? Email { get; set; }
    }
}
