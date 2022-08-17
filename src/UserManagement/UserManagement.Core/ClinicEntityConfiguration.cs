using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserManagement.Core.Domains;

namespace UserManagement.Core;

public class ClinicEntityConfiguration:IEntityTypeConfiguration<Clinic>
{
    public void Configure(EntityTypeBuilder<Clinic> builder)
    {
        builder.HasKey(a => a.Id);
        builder.OwnsMany(p => p.ClinicAddresses, carCategory =>
        {
            carCategory.Property(p => p.Address);
            carCategory.Property(p => p.Street);
            carCategory.Property<int>("Id");
            carCategory.HasKey("Id");
            carCategory.WithOwner().HasForeignKey("ClinicId");
        });
    }
}