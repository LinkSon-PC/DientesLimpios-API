using DientesLimpios.Aplicacion.Contratos.Persistencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Persistencia.UnidadDeTrabajo
{
    public class UnidadDeTrabajoEFCore : IUnidadDeTrabajo
    {
        private readonly DientesLimpiosDBContext context;

        public UnidadDeTrabajoEFCore(DientesLimpiosDBContext context)
        {
            this.context = context;
        }
        public async Task Persistir()
        {
            await context.SaveChangesAsync();
        }

        public Task Reversar()
        {
            return Task.CompletedTask;
        }
    }
}
