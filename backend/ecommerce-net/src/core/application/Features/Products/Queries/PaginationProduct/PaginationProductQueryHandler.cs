using AutoMapper;
using application.Features.Products.Queries.Vms;
using application.Features.Shared.Queries;
using application.Specifications.Products;
using MediatR;
using application.Persistence;
using Application.Specifications.Products;
using domain;

namespace application.Features.Products.Queries.PaginationProduct
{
    public class PaginationProductQueryHandler : IRequestHandler<PaginationProductQuery, PaginationVm<ProductVm>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public PaginationProductQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<PaginationVm<ProductVm>> Handle(PaginationProductQuery request, CancellationToken cancellationToken)
        {
            var productSpecificationParams = new ProductSpecificationParams
            {
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                Search = request.Search,
                Sort = request.Sort,
                CategoryId = request.CategoryId,
                PrecioMax = request.PriceMax,
                PrecioMin = request.PriceMin,

            };
            var spec = new ProductSpecification(productSpecificationParams);
            var products = await unitOfWork.Repository<Product>().GetAllWithSpec(spec);

            var specCount = new ProductForCountingSpecification(productSpecificationParams);
            var totalProducts = await unitOfWork.Repository<Product>().CountAsync(specCount);

            var rounded = Math.Ceiling(Convert.ToDecimal(totalProducts) / Convert.ToDecimal(request.PageSize));
            var totalPages = Convert.ToInt32(rounded);

            var data = mapper.Map<IReadOnlyList<ProductVm>>(products);

            var productsByPage = products.Count();

            var pagination = new PaginationVm<ProductVm>
            {
                Count = totalProducts,
                Data = data,
                PageCount = totalPages,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                ResultByPage = productsByPage
            };

            return pagination;

        }
    }
}
