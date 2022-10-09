using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ProductManagement.Core;

public class ProductDbContext:DbContext
{
    public ProductDbContext(DbContextOptions<ProductDbContext> options)
        : base(options)
    {
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Product>()
            .ToTable("Product", "product")
            .HasKey(p => new { p.Id });
        modelBuilder.Entity<Product>()
            .ForCassandraSetClusterColumns(s => new { s.Name });
    }
}
public class ProductDbContextFactory : IDesignTimeDbContextFactory<ProductDbContext>
{
    public ProductDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ProductDbContext>();
        optionsBuilder.UseCassandra("Contact Points=127.0.0.1;","cv", builder =>
        {
            
        });
        return new ProductDbContext(optionsBuilder.Options);

    }
}