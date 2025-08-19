using AutoMapper;
using application.Exceptions;
using application.Features.Products.Queries.Vms;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using application.Persistence;
using domain;

namespace application.Features.Products.Commands.ChangeStatusProduct
{
    public class ChangeStatusProductCommandHandler : IRequestHandler<ChangeStatusProductCommand, ProductVm>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public ChangeStatusProductCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<ProductVm> Handle(ChangeStatusProductCommand request, CancellationToken cancellationToken)
        {
            var productToUpdate = await unitOfWork.Repository<Product>().GetByIdAsync(request.ProductId);
            if (productToUpdate is null)
            {
                throw new NotFoundException(nameof(Product), request.ProductId);
            }

            productToUpdate.Status = productToUpdate.Status == ProductStatus.Inactivo
                ? ProductStatus.Activo : ProductStatus.Inactivo;

            await unitOfWork.Repository<Product>().UpdateAsync(productToUpdate);

            return mapper.Map<ProductVm>(productToUpdate);
        }
    }
}
