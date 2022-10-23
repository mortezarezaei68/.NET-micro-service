using Framework.Domain.Core;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using UserManagement.Core.Domains;

namespace UserManagement.Core.UserManagementContextConcept;

public class UserManagementContext : IdentityDbContext<User,Role,int>
{
    public UserManagementContext(DbContextOptions<UserManagementContext> options) : base(options)
    {
    }

    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        OnBeforeSaving();
        return base.SaveChanges(acceptAllChangesOnSuccess);
    }

    public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
        CancellationToken cancellationToken = default)
    {
        OnBeforeSaving();
        return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    private void OnBeforeSaving()
    {
        var entities = ChangeTracker.Entries()
            .Where(x => x.Entity is IEntityAudit)
            .ToList();
        UpdateSoftDelete(entities);
        UpdateTimestamps(entities);
    }

    private void UpdateSoftDelete(List<EntityEntry> entries)
    {
        var filtered = entries
            .Where(x => x.State is EntityState.Added or EntityState.Deleted);

        foreach (var entry in filtered)
            switch (entry.State)
            {
                case EntityState.Added:
                    ((IEntityAudit) entry.Entity).IsDeleted = false;
                    break;
                case EntityState.Deleted:
                    entry.State = EntityState.Modified;
                    ((IEntityAudit) entry.Entity).IsDeleted = true;
                    break;
            }
    }

    private void UpdateTimestamps(List<EntityEntry> entries)
    {
        var filtered = entries
            .Where(x => x.State == EntityState.Added
                        || x.State == EntityState.Modified);

        // TODO: Get real current user id
        var currentUserId = 1;

        foreach (var entry in filtered)
        {
            if (entry.State == EntityState.Added)
            {
                ((IEntityAudit) entry.Entity).CreatedAt = DateTime.UtcNow;
                ((IEntityAudit) entry.Entity).CreatedBy = currentUserId;
            }

            ((IEntityAudit) entry.Entity).UpdatedAt = DateTime.UtcNow;
            ((IEntityAudit) entry.Entity).UpdatedBy = currentUserId;
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserConfiguration).Assembly);
        base.OnModelCreating(modelBuilder);
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
}