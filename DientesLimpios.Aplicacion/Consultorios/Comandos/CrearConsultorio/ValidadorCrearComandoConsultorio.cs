using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Aplicacion.Consultorios.Comandos.CrearConsultorio
{
    internal class ValidadorCrearComandoConsultorio: AbstractValidator<ComandoCrearConsultorios>
    {
        public ValidadorCrearComandoConsultorio()
        {
            RuleFor(p => p.Nombre)
                .NotEmpty().WithMessage("El campo {PropertyName} es requerido");
        }
    }
}
