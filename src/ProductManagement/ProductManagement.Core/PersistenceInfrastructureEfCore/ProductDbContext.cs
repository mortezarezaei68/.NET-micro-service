using System.Reflection;
using Microsoft.EntityFrameworkCore;
using ProductManagement.Core.Domains;

namespace ProductManagement.Core.PersistenceInfrastructureEfCore;

public class ProductDbContext:DbContext
{
    public ProductDbContext(DbContextOptions<ProductDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    public DbSet<Product> Products { get; set; }
}