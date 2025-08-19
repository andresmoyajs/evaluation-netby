using System.ComponentModel.DataAnnotations.Schema;
using domain.Common;

namespace domain;

public class Image : BaseDomainModel
{
    [Column(TypeName = "NVARCHAR(4000)")]
    public string? Url { get; set; }
    public int ProductId { get; set; }
    public string? PublicCode { get; set; }

    public virtual Product? Product { get; set; }
}