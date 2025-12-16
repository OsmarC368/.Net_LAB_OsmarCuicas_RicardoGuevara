using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using FluentValidation;

namespace Services.Validators
{
    public class RecipeValidator : AbstractValidator<Recipe>
    {
        public RecipeValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(150)
                .WithMessage("El Nombre es Obligatorio y no debe exceder los 150 caracteres.");

            RuleFor(x => x.Description)
                .NotEmpty()
                .MaximumLength(500)
                .WithMessage("La Descripción es Obligatorio y no debe exceder los 500 caracteres.");

            RuleFor(x => x.Type)
                .NotEmpty()
                .MaximumLength(50)
                .WithMessage("El Tipo es Obligatorio y no debe exceder los 50 caracteres.");
            
            RuleFor(x => x.DifficultyLevel)
                .InclusiveBetween(1, 10)
                .WithMessage("El Nivel de Dificultad debe estar entre 1 y 10.");
        }
    }
}