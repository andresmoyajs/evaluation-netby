using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace application.Features.Products.Commands.SellProduct
{
    public class SellProductCommandValidator : AbstractValidator<SellProductCommand>
    {
        public SellProductCommandValidator()
        {
            RuleFor(p => p.Stock)
                .NotEmpty().WithMessage("El stock no puede estar vacio");
        }
    }
}
