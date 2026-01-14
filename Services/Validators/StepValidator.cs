using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using FluentValidation;

namespace Services.Validators
{
    public class StepValidator : AbstractValidator<Step>
    {
        public StepValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(90)
                .WithMessage("El Nombre es Obligatorio y no debe exceder los 90 caracteres.");

            RuleFor(x => x.Description)
                .NotEmpty()
                .MaximumLength(255)
                .WithMessage("La Descripción es Obligatorio y no debe exceder los 255 caracteres.");

        }
    }
}