
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using domain;

namespace application.Specifications.Products
{
    public class ProductSpecification : BaseSpecification<Product>
    {
        public ProductSpecification(ProductSpecificationParams productParams)
            : base(
                  x =>
                    (string.IsNullOrEmpty(productParams.Search) || x.Name!.Contains(productParams.Search) || x.Description!.Contains(productParams.Search))
                    && (!productParams.CategoryId.HasValue || x.CategoryId == productParams.CategoryId)
                    && (!productParams.PrecioMin.HasValue || x.Price >= productParams.PrecioMin)
                    && (!productParams.PrecioMax.HasValue || x.Price <= productParams.PrecioMax)
                    && (!productParams.Status.HasValue || x.Status == productParams.Status)
             )
        {
            AddInclude(p => p.Images!);

            ApplyPaging(productParams.PageSize * (productParams.PageIndex - 1), productParams.PageSize);

            if (!string.IsNullOrEmpty(productParams.Sort))
            {
                switch (productParams.Sort)
                {
                    case "nameAsc":
                        AddOrderBy(p => p.Name!);
                        break;
                    case "nameDesc":
                        AddOrderByDescending(p => p.Name!);
                        break;
                    case "priceAsc":
                        AddOrderBy(p => p.Price!);
                        break;
                    case "priceDesc":
                        AddOrderByDescending(p => p.Price!);
                        break;
                    default:
                        AddOrderBy(p => p.CreatedDate!);
                        break;
                }
            }
            else
            {
                AddOrderByDescending(p => p.CreatedDate!);
            }

        }
    }
}
