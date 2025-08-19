using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace application.Features.Products.Commands.BuyProduct
{
    public class BuyProductCommandValidator : AbstractValidator<BuyProductCommand>
    {
        public BuyProductCommandValidator()
        {
            RuleFor(p => p.Stock)
                .NotEmpty().WithMessage("El stock no puede estar vacio");
        }
    }
}
