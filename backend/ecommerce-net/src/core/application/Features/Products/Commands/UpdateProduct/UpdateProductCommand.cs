using application.Features.Products.Commands.CreateProduct;
using application.Features.Products.Queries.Vms;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace application.Features.Products.Commands.UpdateProduct
{
    public class UpdateProductCommand : IRequest<ProductVm>
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public int Stock { get; set; }
        public string? CategoryId { get; set; }
        public IReadOnlyList<IFormFile>? Fotos { get; set; }
        public IReadOnlyList<CreateProductImageCommand>? ImageUrls { get; set; }
    }
}
