using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using domain;

namespace application.Specifications.Products
{
    public class ProductSpecificationParams : SpecificationParams
    {
        public int? CategoryId { get; set; }
        public decimal? PrecioMax { get; set; }
        public decimal? PrecioMin { get; set; }
        public int? Rating { get; set; }
        public ProductStatus? Status { get; set; }
    }
}
