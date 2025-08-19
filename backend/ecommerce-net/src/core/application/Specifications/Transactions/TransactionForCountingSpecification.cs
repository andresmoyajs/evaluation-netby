using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using domain;

namespace application.Specifications.Transactions
{
    public class TransactionForCountingSpecification : BaseSpecification<Transaction>
    {
        public TransactionForCountingSpecification(TransactionSpecificationParams orderParams)
            : base(
                    x =>
                    (
                        string.IsNullOrEmpty(orderParams.Username) ||
                        x.CreatedBy!.Contains(orderParams.Username)) &&
                        (!orderParams.Id.HasValue || x.Id == orderParams.Id)
                )
        {

        }
    }
}
