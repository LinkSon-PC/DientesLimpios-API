using DientesLimpios.Aplicacion.Consultorios.Comandos.ActualizarConsultorio;
using DientesLimpios.Aplicacion.Consultorios.Comandos.BorrarConsultorio;
using DientesLimpios.Aplicacion.Consultorios.Comandos.CrearConsultorio;
using DientesLimpios.Aplicacion.Consultorios.Consultas.ObtenerConsultorio;
using DientesLimpios.Aplicacion.Consultorios.Consultas.ObtenerListadoConsultorios;
using DientesLimpios.Aplicacion.Utilidades.Mediador;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Aplicacion
{
    public static class RegistroDeServicioDeAplicacion
    {
        public static IServiceCollection AgregarServicioDeAplicacion(
            this IServiceCollection services)
        {
            services.AddTransient<IMediator, MediadorSimple>();
            services.AddScoped<IRequestHandler<ComandoCrearConsultorios, Guid>,
                        CasoDeUsoCrearConsultorio>();
            services.AddScoped<IRequestHandler<ConsultaObtenerDetalleConsultorio, ConsultorioDetalleDTO>,
                            CasoDeUsoObtenerDetalleConsultorio>();
            services.AddScoped<IRequestHandler<ConsultaObtenerListadoConsultorios, List<ConsultorioListadoDTO>>,
                CasoDeUsoObtenerListadoConsultorios>();
            services.AddScoped<IRequestHandler<ComandoActualizarConsultorio>, CasoDeUsoActualizarConsultorio>();
            services.AddScoped<IRequestHandler<ComandoBorrarConsultorio>, CasoDeUsoBorrarConsultorio>();
            return services;
        }
    }
}
