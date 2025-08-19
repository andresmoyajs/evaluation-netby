using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Mime;
using domain.Common;

namespace domain;

public class Product : BaseDomainModel
{
    [StringLength(100)]
    [Column(TypeName = "NVARCHAR(100)")]
    public string? Name { get; set; }
    [Column(TypeName = "NVARCHAR(4000)")]
    public string? Description { get; set; }
    [Column(TypeName = "DECIMAL(10,2)")]
    public decimal Price { get; set; }

    public int Stock { get; set; }
    public ProductStatus Status { get; set; } = ProductStatus.Activo;
    
    public int CategoryId { get; set; }
    public Category? Category { get; set; }
    
    public virtual ICollection<Image>? Images { get; set; }
    
}