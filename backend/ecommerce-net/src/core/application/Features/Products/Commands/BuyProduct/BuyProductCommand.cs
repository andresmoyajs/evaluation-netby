using application.Features.Products.Queries.Vms;
using MediatR;
using Microsoft.AspNetCore.Http;


namespace application.Features.Products.Commands.BuyProduct
{
    public class BuyProductCommand : IRequest<ProductVm>
    {
        public int Id { get; set; }
        public int Stock { get; set; }
 
    }
}
