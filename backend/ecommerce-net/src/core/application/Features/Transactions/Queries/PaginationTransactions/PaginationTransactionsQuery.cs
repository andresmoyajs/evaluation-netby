using application.Features.Transactions.Vms;
using application.Features.Shared.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace application.Features.Transactions.Queries.PaginationTransactions
{
    public class PaginationTransactionsQuery : PaginationBaseQuery, IRequest<PaginationVm<TransactionVm>>
    {
        public int? Id { get; set; }
        public string? Username { get; set; }

    }
}
