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
        public DientesLimpiosDBContext(DbContextOptions<DientesLimpiosDBContext> options) : base(options)
        {
        }

        protected DientesLimpiosDBContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DientesLimpiosDBContext).Assembly);
        }

        public DbSet<Consultorio> Consultorios { get; set; }
    }
}
