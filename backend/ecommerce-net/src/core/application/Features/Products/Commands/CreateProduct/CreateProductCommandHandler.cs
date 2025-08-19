using AutoMapper;
using application.Features.Products.Queries.Vms;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using application.Persistence;
using domain;

namespace application.Features.Products.Commands.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ProductVm>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public CreateProductCommandHandler(IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<ProductVm> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var productEntity = mapper.Map<Product>(request);

            await unitOfWork.Repository<Product>().AddAsync(productEntity);

            if ((request.ImageUrls is not null) && request.ImageUrls.Count > 0)
            {
                request.ImageUrls.Select(c =>
                {
                    c.ProductId = productEntity.Id;
                    return c;
                }).ToList();

                var images = mapper.Map<List<Image>>(request.ImageUrls);
                unitOfWork.Repository<Image>().AddRange(images);
                await unitOfWork.Complete();
            }

            return mapper.Map<ProductVm>(productEntity);
        }
    }
}
