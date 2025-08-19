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

namespace application.Features.Products.Commands.DeleteProduct
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, ProductVm>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public DeleteProductCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<ProductVm> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var productToUpdate = await unitOfWork.Repository<Product>().GetByIdAsync(request.ProductId);
            if (productToUpdate is null)
            {
                throw new NotFoundException(nameof(Product), request.ProductId);
            }
            await unitOfWork.Repository<Product>().DeleteAsync(productToUpdate);

            return mapper.Map<ProductVm>(productToUpdate);
        }
    }
}
