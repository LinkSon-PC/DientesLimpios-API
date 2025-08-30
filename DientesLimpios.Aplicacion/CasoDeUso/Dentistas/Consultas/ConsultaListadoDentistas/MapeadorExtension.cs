using DientesLimpios.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Aplicacion.CasoDeUso.Dentistas.Consultas.ConsultaListadoDentistas
{
    public static class MapeadorExtension
    {
        public static DentistaListadoDTO ADto(this Dentista dentista) 
        {
            var dto = new DentistaListadoDTO 
            { 
                Id = dentista.Id, 
                Nombre = dentista.Nombre, 
                Email = dentista.Email.Valor 
            };
            return dto;
        }
    }
}
