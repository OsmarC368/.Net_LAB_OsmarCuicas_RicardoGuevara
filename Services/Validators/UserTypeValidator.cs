using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using FluentValidation;

namespace Services.Validators
{
    public class UserTypeValidator : AbstractValidator<UserType>
    {
        public UserTypeValidator()
        {
            RuleFor(x => x.name)
                .NotEmpty()
                .MaximumLength(90);

            RuleFor(x => x.description)
                .NotEmpty()
                .MaximumLength(255);
                
        }   
    }
}