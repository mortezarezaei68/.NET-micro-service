using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserManagement.Core.Domains;

namespace UserManagement.Core;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(a => a.Id);
        // builder.OwnsMany(p => p.UserRoles, carCategory =>
        // {
        //     carCategory.Property(p => p.RoleId);
        //     carCategory.Property<int>("Id");
        //     carCategory.HasKey("Id");
        //     carCategory.WithOwner().HasForeignKey("UserId");
        // });
    }
}