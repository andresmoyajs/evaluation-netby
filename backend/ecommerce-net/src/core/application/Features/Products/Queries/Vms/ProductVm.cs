using application.Features.Images.Queries.Vms;
using application.Models.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using domain;

namespace application.Features.Products.Queries.Vms
{
    public class ProductVm
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public decimal Price { get; set; }

        public string? Description { get; set; }

        public int Stock { get; set; }


        public virtual ICollection<ImageVm>? Images { get; set; }

        public int CategoryId { get; set; }

        public string? CategoryName { get; set; }

        public ProductStatus Status { get; set; }
        public string StatusLabel
        {
            get
            {
                switch (Status)
                {

                    case ProductStatus.Activo:
                        {
                            return ProductStatusLabel.ACTIVO;
                        }

                    case ProductStatus.Inactivo:
                        {
                            return ProductStatusLabel.INACTIVO;
                        }

                    default: return ProductStatusLabel.INACTIVO;
                }
            }
            set { }

        }

    }
}
