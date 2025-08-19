using domain;
using domain.Common;
using Microsoft.EntityFrameworkCore;

namespace infrastructure.Persistence;

public class TransactionDbContext : DbContext
{
    public TransactionDbContext(DbContextOptions<TransactionDbContext> options) : base(options)
    {

    }
    
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken=default)
    {
        var userName = "system";
        foreach (var entry in ChangeTracker.Entries<BaseDomainModel>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedDate = DateTime.Now;
                    entry.Entity.CreatedBy = userName;
                    break;

                case EntityState.Modified:
                    entry.Entity.LastModifiedDate = DateTime.Now;
                    entry.Entity.LastModifiedBy = userName;
                    break;
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }
    public DbSet<Transaction>? Transactions { get; set; }
}