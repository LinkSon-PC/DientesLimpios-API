using DientesLimpios.Aplicacion.Contratos.Notificaciones;
using DientesLimpios.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Aplicacion.CasoDeUso.Citas.Comandos.CrearCita
{
    public static class MpaeadorExtensions
    {
        public static ConfirmacionCitaDTO ADto(this Cita cita)
        {
            return new ConfirmacionCitaDTO
            {
                Id = cita.Id,
                Fecha = cita.IntervaloDeTiempo.Inicio,
                Paciente = cita.Paciente!.Nombre,
                Paciente_Email = cita.Paciente.Email.Valor,
                Consultorio = cita.Consultorio!.Nombre,
                Dentista = cita.Dentista!.Nombre
            };
        }
    }
}
