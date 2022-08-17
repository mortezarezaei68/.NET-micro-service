using Framework.Context;
using Microsoft.EntityFrameworkCore;
using UserManagement.Core.Domains;

namespace UserManagement.Core.UserManagementContextConcept;

public class UserManagementContext : CoreDbContext
{
    public UserManagementContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserConfiguration).Assembly);
        base.OnModelCreating(modelBuilder);
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Clinic> Clinics { get; set; }
}