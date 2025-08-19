using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using application.Persistence;
using domain;

namespace application.Features.Products.Queries.GetProductList
{
    public class GetProductListQueryHandler : IRequestHandler<GetProductListQuery, IReadOnlyList<Product>>
    {
        private readonly IUnitOfWork unitOfWork;

        public GetProductListQueryHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<IReadOnlyList<Product>> Handle(GetProductListQuery request, CancellationToken cancellationToken)
        {
            var includes = new List<Expression<Func<Product, object>>>();
            includes.Add(p => p.Images!);

            var products = await unitOfWork.Repository<Product>().GetAsync(
                null,
                x => x.OrderBy(y => y.Name),
                includes,
                true
            );

            return products;
        }
    }
}
