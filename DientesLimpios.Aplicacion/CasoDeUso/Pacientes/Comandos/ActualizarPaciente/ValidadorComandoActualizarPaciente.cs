using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Aplicacion.CasoDeUso.Pacientes.Comandos.ActualizarPaciente
{
    public class ValidadorComandoActualizarPaciente  : AbstractValidator<ComandoActualizarPaciente>
    {
        public ValidadorComandoActualizarPaciente()
        {
            RuleFor(p => p.Nombre)
                .NotEmpty().WithMessage("El campo {PropertyName} es requerido")
                .MaximumLength(250).WithMessage("La longitud del campo {PropertyName} debe ser menor o igual {MaxLength}");

            RuleFor(p => p.Email)
                .NotEmpty().WithMessage("El campo {PropertyName} es requerido")
                .MaximumLength(254).WithMessage("La logintud del campo {PropertyName} debe ser menor o igaual a {MaxLengtk}")
                .EmailAddress().WithMessage("El formato de email no es válido");
        }
    }
}
