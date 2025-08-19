using application.Features.Products.Queries.Vms;
using MediatR;
using Microsoft.AspNetCore.Http;


namespace application.Features.Products.Commands.SellProduct
{
    public class SellProductCommand : IRequest<ProductVm>
    {
        public int Id { get; set; }
        public int Stock { get; set; }
 
    }
}
