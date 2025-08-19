using System.Runtime.Serialization;

namespace domain;

public enum TransactionType
{
    [EnumMember(Value = "Compra")]
    Compra,
    [EnumMember(Value ="Venta")]
    Venta
}