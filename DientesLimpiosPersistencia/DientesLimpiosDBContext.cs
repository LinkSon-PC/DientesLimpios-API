using DientesLimpios.Aplicacion.Contratos.Identidad;
using DientesLimpios.Dominio.Comunes;
using DientesLimpios.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Persistencia
{
    public class DientesLimpiosDBContext : DbContext
    {
        private readonly IServicioUsuarios servicioUsuarios;

        public DientesLimpiosDBContext(DbContextOptions<DientesLimpiosDBContext> options,
            IServicioUsuarios servicioUsuarios) 
            : base(options)
        {
            this.servicioUsuarios = servicioUsuarios;
        }

        protected DientesLimpiosDBContext()
        {
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            if(servicioUsuarios is not null)
            {
                foreach (var entry in ChangeTracker.Entries<EntidadAuditable>())
                {
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            entry.Entity.FechaCreacion = DateTime.UtcNow;
                            entry.Entity.CreadoPor = servicioUsuarios.ObtenerUsuarioId();
                            break;
                        case EntityState.Modified:
                            entry.Entity.UltimaFechaModificacion = DateTime.UtcNow;
                            entry.Entity.UltimaModificacionPor = servicioUsuarios.ObtenerUsuarioId();
                            break;
                    }
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DientesLimpiosDBContext).Assembly);
        }

        public DbSet<Consultorio> Consultorios { get; set; }
        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<Dentista> Dentistas { get; set; }
        public DbSet<Cita> Citas { get; set; }
    }
}
