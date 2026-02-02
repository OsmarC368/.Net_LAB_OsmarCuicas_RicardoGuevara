using System;
using System.Collections.Generic;
using System.Data;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using FluentValidation;

namespace Services.Validators
{
    public class StepUserValidator: AbstractValidator<StepUser>
    {
        public StepUserValidator()
        {
            RuleFor(x => x.comment)
                .MaximumLength(255)
                .WithMessage("El comentario no debe exceder los 255 caracteres.");
        }
    }
}