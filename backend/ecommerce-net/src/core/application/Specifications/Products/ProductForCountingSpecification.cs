using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using application.Specifications;
using application.Specifications.Products;
using domain;

namespace Application.Specifications.Products
{
    public class ProductForCountingSpecification : BaseSpecification<Product>
    {
        public ProductForCountingSpecification(ProductSpecificationParams productParams)
            : base(
                    x =>
                    (string.IsNullOrEmpty(productParams.Search) || x.Name!.Contains(productParams.Search) || x.Description!.Contains(productParams.Search))
                    && (!productParams.CategoryId.HasValue || x.CategoryId == productParams.CategoryId)
                    && (!productParams.PrecioMin.HasValue || x.Price >= productParams.PrecioMin)
                    && (!productParams.PrecioMax.HasValue || x.Price <= productParams.PrecioMax)
                    && (!productParams.Status.HasValue || x.Status == productParams.Status)

                )
        {

        }
    }
}
