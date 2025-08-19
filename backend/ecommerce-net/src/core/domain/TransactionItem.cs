using System.ComponentModel.DataAnnotations.Schema;
using domain.Common;

namespace domain;

public class TransactionItem: BaseDomainModel
{
    public Product? Product { get; set; }
    public int ProductId { get; set; }
    [Column(TypeName = "DECIMAL(10,2)")]
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public Transaction? Transaction { get; set; }
    public int TransactionId { get; set; }
    public int ProductItemId { get; set; }
    public string? ProductName { get; set; }
    public string? ImagenUrl { get; set; }
}