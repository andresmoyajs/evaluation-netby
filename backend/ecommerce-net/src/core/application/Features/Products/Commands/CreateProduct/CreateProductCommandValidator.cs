using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace application.Features.Products.Commands.CreateProduct
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("El nombre no puede estar en blanco")
                .MaximumLength(50).WithMessage("El nombre no puede exceeder de los 50 caracteres");

            RuleFor(p => p.Description)
                .NotEmpty().WithMessage("La descripcion no puede estar vacia");


            RuleFor(p => p.Stock)
                .NotEmpty().WithMessage("El stock no puede estar vacio");


            RuleFor(p => p.Price)
                .NotEmpty().WithMessage("El precio no puede estar vacio");

        }
    }
}
