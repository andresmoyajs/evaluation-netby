using domain;
using domain.Common;
using Microsoft.EntityFrameworkCore;

namespace infrastructure.Persistence;

public class ProductDbContext : DbContext
{
    public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options)
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

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Category>()
            .HasMany(p => p.Products)
            .WithOne(r => r.Category)
            .HasForeignKey(r => r.CategoryId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.Entity<Product>()
            .HasMany(p => p.Images)
            .WithOne(r => r.Product)
            .HasForeignKey(r => r.ProductId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
        
    }

    public DbSet<Product>? Products { get; set; }
    public DbSet<Image>? Images { get; set; }
    public DbSet<Category>? Categories { get; set; }
}