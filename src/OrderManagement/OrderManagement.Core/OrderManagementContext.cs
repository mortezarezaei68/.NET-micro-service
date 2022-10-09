using Framework.Context;
using MassTransit;
using MassTransit.EntityFrameworkCoreIntegration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OrderManagement.Core;

public class OrderManagementContext:SagaDbContext
{
    public OrderManagementContext(DbContextOptions<OrderManagementContext> options)
        : base(options)
    {
    }


    protected override IEnumerable<ISagaClassMap> Configurations
    {
        get { yield return new RegistrationStateMap(); }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.AddInboxStateEntity();
        modelBuilder.AddOutboxMessageEntity();
        modelBuilder.AddOutboxStateEntity();
    }
    
}
public class RegistrationStateMap :
    SagaClassMap<OrderState>
{
    protected override void Configure(EntityTypeBuilder<OrderState> entity, ModelBuilder model)
    {
        entity.Property(x => x.CreatedDate);
        
        entity.Property(x => x.UpdatedDate);
        entity.Property(x => x.EventId);
        entity.Property(x => x.State);
    }
}

