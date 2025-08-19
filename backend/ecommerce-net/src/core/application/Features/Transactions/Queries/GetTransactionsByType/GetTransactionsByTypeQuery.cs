using application.Features.Transactions.Vms;
using domain;
using MediatR;

namespace application.Features.Transactions.Queries.GetTransactionsByType
{
    public class GetTransactionsByTypeQuery : IRequest<TransactionVm>
    {
        public TransactionType Type { get; set; }

        public GetTransactionsByTypeQuery(TransactionType type)
        {
            this.Type = type == 0 ? throw new ArgumentNullException(nameof(type)) : type;
        }
    }
}
