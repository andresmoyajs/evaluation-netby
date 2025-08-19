using System.ComponentModel.DataAnnotations.Schema;
using domain.Common;

namespace domain;

public class Category: BaseDomainModel
{
    [Column(TypeName = "NVARCHAR(100)")]
    public string? Name { get; set; }
    public virtual ICollection<Product>? Products { get; set; }
}