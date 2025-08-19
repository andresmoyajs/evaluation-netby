using System.Runtime.Serialization;

namespace domain;

public enum ProductStatus
{
    [EnumMember(Value = "Producto Inactivo")]
    Inactivo,
    [EnumMember(Value ="Producto Activo")]
    Activo
}