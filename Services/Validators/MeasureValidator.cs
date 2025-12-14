using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using FluentValidation;

namespace Services.Validators
{
    public class MeasureValidator : AbstractValidator<Measure>
    {
        public MeasureValidator()
        {
            RuleFor(x => x.name)
                .NotEmpty()
                .MaximumLength(90)
                .WithMessage("El Nombre es Obligatorio y no debe exceder los 90 caracteres.");

            RuleFor(x => x.symbol)
                .NotEmpty()
                .MaximumLength(255)
                .WithMessage("El Símbolo es Obligatorio y no debe exceder los 90 caracteres.");
                
        } 
    }
}