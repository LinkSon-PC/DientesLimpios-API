﻿using DientesLimpios.Aplicacion.Contratos.Repositorios;
using DientesLimpios.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Persistencia.Repositorios
{
    public class RepositorioConsultorios: Repositorio<Consultorio>, IRepositorioConsultorios
    {
        public RepositorioConsultorios(DientesLimpiosDBContext context)
            : base(context) 
        {
            
        }
    }
}
