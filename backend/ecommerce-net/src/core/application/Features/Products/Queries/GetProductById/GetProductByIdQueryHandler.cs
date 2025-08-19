using AutoMapper;
using application.Features.Products.Queries.Vms;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using application.Persistence;
using domain;

namespace application.Features.Products.Queries.GetProductById
{
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductVm>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public GetProductByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<ProductVm> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var includes = new List<Expression<Func<Product, object>>>();
            includes.Add(p => p.Images!);

            var product = await unitOfWork.Repository<Product>().GetEntityAsync(
                x => x.Id == request.ProductId,
                includes,
                true
            );

            return mapper.Map<ProductVm>(product);

        }
    }
}
