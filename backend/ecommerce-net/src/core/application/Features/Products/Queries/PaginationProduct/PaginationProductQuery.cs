using application.Features.Products.Queries.Vms;
using application.Features.Shared.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using domain;

namespace application.Features.Products.Queries.PaginationProduct
{
    public class PaginationProductQuery : PaginationBaseQuery, IRequest<PaginationVm<ProductVm>>
    {
        public int? CategoryId { get; set; }

        public decimal? PriceMax { get; set; }

        public decimal? PriceMin { get; set; }

        public ProductStatus? Status { get; set; }
    }
}
