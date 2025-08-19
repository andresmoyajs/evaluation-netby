using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace domain.Configuration;

public class TransactionItemConfiguration: IEntityTypeConfiguration<TransactionItem>
{
    public void Configure(EntityTypeBuilder<TransactionItem> builder)
    {
        builder.Property(s => s.Price)
            .HasColumnType("decimal(10,2)");
    }
}