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

namespace application.Features.Products.Commands.UpdateProduct
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ProductVm>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public UpdateProductCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<ProductVm> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var productToUpdate = await unitOfWork.Repository<Product>().GetByIdAsync(request.Id);
            if (productToUpdate is null)
            {
                throw new NotFoundException(nameof(Product), request.Id);
            }

            mapper.Map(request, productToUpdate, typeof(UpdateProductCommand), typeof(Product));

            await unitOfWork.Repository<Product>().UpdateAsync(productToUpdate);

            if ((request.ImageUrls is not null) && request.ImageUrls.Count > 0)
            {
                var imagesToRemove = await unitOfWork.Repository<Image>().GetAsync(
                    x => x.ProductId == request.Id
                );

                unitOfWork.Repository<Image>().DeleteRange(imagesToRemove);

                request.ImageUrls.Select(
                    c =>
                    {
                        c.ProductId = request.Id;
                        return c;
                    }
                ).ToList();

                var images = mapper.Map<List<Image>>(request.ImageUrls);

                unitOfWork.Repository<Image>().AddRange(images);

                await unitOfWork.Complete();
            }

            return mapper.Map<ProductVm>(productToUpdate);
        }
    }
}
