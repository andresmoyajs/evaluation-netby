using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace application.Models.Transaction
{
	public static class TransactionStatusLabel
	{
		public const string ERROR = nameof(ERROR);
	}
	
	public static class TransactionTypeLabel
	{
		public const string COMPRA = nameof(COMPRA);
		public const string VENTA = nameof(VENTA);
	}
}
