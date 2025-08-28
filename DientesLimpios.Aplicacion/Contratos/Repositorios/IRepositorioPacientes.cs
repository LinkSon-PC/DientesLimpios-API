using DientesLimpios.Aplicacion.CasoDeUso.Pacientes.Consultas.ObtenerListadoPacientes;
using DientesLimpios.Aplicacion.Contratos.Repositorios;
using DientesLimpios.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Persistencia.Repositorios
{
    public interface IRepositorioPacientes : IRepositorio<Paciente>
    {
        Task<IEnumerable<Paciente>> ObtenerFiltrado(FiltroPacienteDTO filtro);
    }
}
