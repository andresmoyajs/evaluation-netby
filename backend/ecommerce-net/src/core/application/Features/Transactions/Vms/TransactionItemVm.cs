using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace application.Features.Transactions.Vms
{
    public class TransactionItemVm
    {
        public int ProductId { get; set; }
        public decimal Precio { get; set; }
        public int Cantidad { get; set; }
        public int OrderId { get; set; }
        public int ProductItemId { get; set; }
        public string? ProductNombre { get; set; }
        public string? ImageUrl { get; set; }

    }
}
