using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using FluentValidation;

namespace Services.Validators
{
    public class IngredientsPerRecipeValidator : AbstractValidator<IngredientsPerRecipe>
    {
        public IngredientsPerRecipeValidator()
        {
            RuleFor(x => x.amount)
                .GreaterThan(0)
                .WithMessage("La cantidad debe ser mayor que cero.");
        }
    }
}