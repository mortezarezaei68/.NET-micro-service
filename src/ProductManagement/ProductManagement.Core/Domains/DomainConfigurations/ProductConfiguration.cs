using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ProductManagement.Core.Domains.DomainConfigurations;

public class ProductConfiguration:IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(a => a.Id);
        builder.OwnsMany(p => p.ProductDetails, orderTransactions =>
        {
            orderTransactions.Property(p => p.Key);
            orderTransactions.Property(p => p.Value);
            orderTransactions.ToTable("ProductDetails");
            orderTransactions.Property<int>("Id");
            orderTransactions.HasKey("Id");
            orderTransactions.WithOwner().HasForeignKey("ProductId");
        });
    }
}

