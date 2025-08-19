using System.ComponentModel.DataAnnotations.Schema;
using domain.Common;

namespace domain;

public class Transaction : BaseDomainModel
{
    public int ProductId { get; set; }
    public TransactionType? Type { get; set; } = TransactionType.Compra;
    
    [Column(TypeName = "DECIMAL(10,2)")]
    public decimal Tax { get; set; }
    
    [Column(TypeName = "DECIMAL(10,2)")]
    public decimal Subtotal { get; set; }
    
    [Column(TypeName = "DECIMAL(10,2)")]
    public decimal Total { get; set; }
    
    public int Quantity { get; set; }

}