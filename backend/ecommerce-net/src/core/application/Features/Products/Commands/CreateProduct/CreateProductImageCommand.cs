using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace application.Features.Products.Commands.CreateProduct
{
    public class CreateProductImageCommand
    {
        public string? Url { get; set; }
        public int ProductId { get; set; }
        public string? PublicCode { get; set; }
    }
}
