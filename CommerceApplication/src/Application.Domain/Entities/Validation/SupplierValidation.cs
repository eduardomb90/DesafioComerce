using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Domain.Entities.Validation
{
    public class SupplierValidation : AbstractValidator<Supplier>
    {
        public SupplierValidation()
        {
            //RuleFor(x => x.Email)
            //    .NotEmpty()
            //    .WithMessage("O e-mail e obrigatorio.");
        }
    }
}
