using application.Features.Transactions.Vms;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using domain;

namespace application.Features.Transactions.Command.CreateTransaction
{
    public class CreateTransactionCommand : IRequest<TransactionVm>
    {
        public int ProductId { get; set; }
        public TransactionType Type { get; set; }
        public decimal Tax{ get; set; }
        public decimal Subtotal{ get; set; }
        public decimal Total{ get; set; }
        public int Quantity { get; set; }
        
    }
}
