using application.Features.Transactions.Vms;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using domain;

namespace application.Features.Transactions.Command.UpdateTransaction
{
    public class UpdateTransactionCommand : IRequest<TransactionVm>
    {
        public int TransactionId { get; set; }
        public TransactionStatus Status { get; set; }
    }
}
