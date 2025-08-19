using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using domain;

namespace application.Specifications.Transactions
{
    public class TransactionSpecification : BaseSpecification<Transaction>
    {
        public TransactionSpecification(TransactionSpecificationParams orderParams)
            : base(
                    x =>
                    (
                        string.IsNullOrEmpty(orderParams.Username) ||
                        x.CreatedBy!.Contains(orderParams.Username)) &&
                        (!orderParams.Id.HasValue || x.Id == orderParams.Id)
                )
        {

            ApplyPaging(orderParams.PageSize * (orderParams.PageIndex - 1), orderParams.PageSize);

            if (!string.IsNullOrEmpty(orderParams.Sort))
            {
                switch (orderParams.Sort)
                {
                    case "createDateAsc":
                        AddOrderBy(p => p.CreatedDate!);
                        break;
                    case "createDateDesc":
                        AddOrderByDescending(p => p.CreatedDate!);
                        break;
                    default:
                        AddOrderBy(p => p.CreatedDate!);
                        break;
                }
            }
            else
            {
                AddOrderByDescending(p => p.CreatedDate!);
            }

        }
    }
}
