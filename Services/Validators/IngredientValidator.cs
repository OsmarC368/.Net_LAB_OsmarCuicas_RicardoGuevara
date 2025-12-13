using Core.Entities;
using FluentValidation;

namespace Services.Validators
{
    public class IngredientValidator : AbstractValidator<Ingredient>
    {
        public IngredientValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("El nombre es obligatorio")
                .MaximumLength(255).WithMessage("El nombre no puede superar los 255 caracteres");

            RuleFor(x => x.Type)
                .NotEmpty().WithMessage("El tipo es obligatorio")
                .MaximumLength(255).WithMessage("El tipo no puede superar los 255 caracteres");
        }
    }
}
