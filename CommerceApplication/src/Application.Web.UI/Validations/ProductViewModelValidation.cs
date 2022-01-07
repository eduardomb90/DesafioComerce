using Application.Web.UI.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Web.UI.Validations
{
    public class ProductViewModelValidation : AbstractValidator<ProductViewModel>
    {
        public ProductViewModelValidation()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("This field is required.");

            RuleFor(x => x.BarCode)
                .NotEmpty()
                .WithMessage("This field is required.");

            RuleFor(x => x.PricePurchase)
                .NotEmpty()
                .WithMessage("This field is required.");

            RuleFor(x => x.PriceSales)
                .NotEmpty()
                .WithMessage("This field is required.");
        }
    }
}
