using application.Features.Products.Queries.Vms;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace application.Features.Products.Commands.ChangeStatusProduct
{
    public class ChangeStatusProductCommand : IRequest<ProductVm>
    {
        public int ProductId { get; set; }

        public ChangeStatusProductCommand(int productId)
        {
            ProductId = productId == 0 ? throw new ArgumentException(nameof(productId)) : productId;
        }
    }
}
