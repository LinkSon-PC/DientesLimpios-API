using DientesLimpios.Dominio.Comunes;
using DientesLimpios.Dominio.Excepciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Dominio.Entidades
{
    public class Consultorio : EntidadAuditable
    {
        public Guid Id { get; private set; }
        public string Nombre { get; private set; } = null!;

        public Consultorio(string nombre)
        {
            AplicarReglasDENegocioNombre(nombre);
            Nombre = nombre;
            Id = Guid.CreateVersion7();
        }

        public void ActualizarNombre(string nombre)
        {
            AplicarReglasDENegocioNombre(nombre);
            Nombre = nombre;
        }

        private void AplicarReglasDENegocioNombre(string nombre)
        {
            if (string.IsNullOrEmpty(nombre))
                throw new ExcepcionDeReglaDeNegocio($"El {nameof(nombre)} es obligatorio");
        }
    }
}
