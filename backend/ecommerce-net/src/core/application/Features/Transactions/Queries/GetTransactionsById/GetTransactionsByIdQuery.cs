using application.Features.Transactions.Vms;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace application.Features.Transactions.Queries.GetTransactionsById
{
    public class GetTransactionsByIdQuery : IRequest<TransactionVm>
    {
        public int TransactionId { get; set; }

        public GetTransactionsByIdQuery(int transactionId)
        {
            this.TransactionId = transactionId == 0 ? throw new ArgumentNullException(nameof(transactionId)) : transactionId;
        }
    }
}
