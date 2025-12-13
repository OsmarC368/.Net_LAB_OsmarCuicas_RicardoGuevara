using Core.Entities;
using FluentValidation;

namespace Services.Validators
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("El nombre es obligatorio")
                .MaximumLength(255).WithMessage("El nombre no puede superar los 255 caracteres");

            RuleFor(x => x.Lastname)
                .NotEmpty().WithMessage("El apellido es obligatorio")
                .MaximumLength(255).WithMessage("El apellido no puede superar los 255 caracteres");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("El email es obligatorio")
                .MaximumLength(255).WithMessage("El email no puede superar los 255 caracteres")
                .EmailAddress().WithMessage("El email no tiene un formato válido");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("La contraseña es obligatoria")
                .MaximumLength(255).WithMessage("La contraseña no puede superar los 255 caracteres")
                .MinimumLength(6).WithMessage("La contraseña debe tener al menos 6 caracteres");

            RuleFor(x => x.UserTypeID)
                .GreaterThan(0).WithMessage("Debe seleccionar un tipo de usuario válido");
        }
    }
}
