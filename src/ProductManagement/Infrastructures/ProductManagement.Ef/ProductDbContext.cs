using System.Reflection;
using MassTransit;
using MassTransit.EntityFrameworkCoreIntegration;
using Microsoft.EntityFrameworkCore;
using ProductManagement.Domains;

namespace ProductManagement.Ef;

public class ProductDbContext:SagaDbContext
{
    public ProductDbContext(DbContextOptions<ProductDbContext> options, IEnumerable<ISagaClassMap> configurations)
        : base(options)
    {
        Configurations = configurations;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        modelBuilder.AddInboxStateEntity();
        modelBuilder.AddOutboxMessageEntity();
        modelBuilder.AddOutboxStateEntity();
        base.OnModelCreating(modelBuilder);
    }

    protected override IEnumerable<ISagaClassMap> Configurations { get; }

    public DbSet<Product> Products { get; set; }
}