using Application.Web.UI.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Web.UI.Validations
{
    public class SupplierViewModelValidation : AbstractValidator<SupplierViewModel>
    {
        public SupplierViewModelValidation()
        {
            RuleFor(x => x.Type)
                .NotEmpty();
            
            //RuleFor(x => x.Email.EmailAddress)
            //    .NotEmpty()
            //    .WithMessage("Email address is required.")
            //    .EmailAddress()
            //    .WithMessage("A valid email address is required.");

            //RuleFor(x => x.Address.Street)
            //    .NotEmpty()
            //    .WithMessage("This field is required.");

            //RuleFor(x => x.Address.Number)
            //    .NotEmpty()
            //    .WithMessage("This field is required.");

            //RuleFor(x => x.Address.Neighborhood)
            //    .NotEmpty()
            //    .WithMessage("This field is required.");

            //RuleFor(x => x.Address.City)
            //    .NotEmpty()
            //    .WithMessage("This field is required.");

            //RuleFor(x => x.Address.State)
            //    .NotEmpty()
            //    .WithMessage("This field is required.");

            //RuleFor(x => x.Address.ZipCode)
            //    .NotEmpty()
            //    .WithMessage("This field is required.")
            //    .Length(8,8)
            //    .WithMessage("Zipcode must have 8 digits")
            //    .Matches(@"^[0-9]{8}$")
            //    .WithMessage("Only numbers");

            //RuleFor(x => x.CellPhone.Ddd)
            //    .NotEmpty()
            //    .WithMessage("This field is required.")
            //    .Length(2, 2)
            //    .Matches(@"^[1-9]{2}$")
            //    .WithMessage("DDD must have 2 digits");

            //RuleFor(x => x.CellPhone.Number)
            //    .NotEmpty()
            //    .WithMessage("This field is required.")
            //    .Length(9, 9)
            //    .Matches(@"^9[1-9]{1}[0-9]{7}$")
            //    .WithMessage("Please type a valid cellphone number.");

            //RuleFor(x => x.HomePhone.Ddd)
            //    .Length(2, 2)
            //    .Matches(@"^[1-9]{2}$")
            //    .WithMessage("DDD must have 2 digits");

            //RuleFor(x => x.HomePhone.Number)
            //    .Length(8, 9)
            //    .Matches(@"^(?:[2-8]|9[1-9])[0-9]{3}[0-9]{4}$")
            //    .WithMessage("Please type a valid phone number.");

            //RuleFor(x => x.Phone.Ddd)
            //    .Length(2, 2)
            //    .Matches(@"^[1-9]{2}$")
            //    .WithMessage("DDD must have 2 digits");

            //RuleFor(x => x.Phone.Number)
            //    .Length(8, 9)
            //    .Matches(@"^(?:[2-8]|9[1-9])[0-9]{3}[0-9]{4}$")
            //    .WithMessage("Please type a valid phone number.");

            RuleFor(x => x.FantasyName)
                .NotEmpty()
                .WithMessage("This field is required.");

            //RuleFor(x => x.FullName)
            //    .
        }
    }
}
