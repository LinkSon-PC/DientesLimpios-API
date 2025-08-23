using DientesLimpios.Aplicacion.Utilidades.Mediador;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Aplicacion.Consultorios.Comandos.CrearConsultorio
{
    public class ComandoCrearConsultorios: IRequest<Guid>
    {
        public required string Nombre { get; set; }
    }
}
