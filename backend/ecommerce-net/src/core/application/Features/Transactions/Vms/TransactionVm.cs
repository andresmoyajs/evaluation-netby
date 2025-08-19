using application.Models.Transaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using domain;

namespace application.Features.Transactions.Vms
{
    public class TransactionVm
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Tax { get; set; }
        public decimal Total { get; set; }
        public DateTime? CreatedDate { get; set; }

        public TransactionType Type { get; set; }

        public int Quantity { get; set; }

        public string? TransactionLabel
        {
            get
            {
                switch (Type)
                {
                    case TransactionType.Compra:
                        return TransactionTypeLabel.COMPRA;
                    case TransactionType.Venta:
                        return TransactionTypeLabel.VENTA;
                    default:
                        return TransactionStatusLabel.ERROR;
                }
            }

        }
    }
}
