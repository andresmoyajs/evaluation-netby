using application.Features.Transactions.Vms;
using MediatR;

namespace application.Features.Transactions.Queries.GetTransactionsByProductId
{
    public class GetTransactionsByProductIdQuery : IRequest<IReadOnlyList<TransactionVm>>
    {
        public int ProductId { get; set; }

        public GetTransactionsByProductIdQuery(int productId)
        {
            this.ProductId = productId == 0 ? throw new ArgumentNullException(nameof(productId)) : productId;
        }
    }
}
