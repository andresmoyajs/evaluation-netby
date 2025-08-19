using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using domain;

namespace application.Features.Products.Queries.GetProductList
{
    public class GetProductListQuery : IRequest<IReadOnlyList<Product>>
    {

    }
}
