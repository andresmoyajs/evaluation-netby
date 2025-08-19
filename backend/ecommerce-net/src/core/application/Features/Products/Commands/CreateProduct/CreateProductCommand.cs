using application.Features.Products.Queries.Vms;
using MediatR;
using Microsoft.AspNetCore.Http;


namespace application.Features.Products.Commands.CreateProduct
{
    public class CreateProductCommand : IRequest<ProductVm>
    {
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public int Stock { get; set; }
        public string? CategoryId { get; set; }
        public IReadOnlyList<IFormFile>? Fotos { get; set; }
        public IReadOnlyList<CreateProductImageCommand>? ImageUrls { get; set; }
    }
}
