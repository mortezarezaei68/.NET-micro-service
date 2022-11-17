using MassTransit;
using MassTransit.EntityFrameworkCoreIntegration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BasketManagement.Core;

public class BasketManagementContext:SagaDbContext
{
    public BasketManagementContext(DbContextOptions<BasketManagementContext> options, IEnumerable<ISagaClassMap> configurations)
        : base(options)
    {
        Configurations = configurations;
    }


    // protected override IEnumerable<ISagaClassMap> Configurations
    // {
    //     get { yield return new RegistrationStateMap(); }
    // }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.AddInboxStateEntity();
        modelBuilder.AddOutboxMessageEntity();
        modelBuilder.AddOutboxStateEntity();
    }

    protected override IEnumerable<ISagaClassMap> Configurations { get; }
}
// public class RegistrationStateMap :
//     SagaClassMap<OrderState>
// {
//     protected override void Configure(EntityTypeBuilder<OrderState> entity, ModelBuilder model)
//     {
//         entity.Property(x => x.CreatedDate);
//         
//         entity.Property(x => x.UpdatedDate);
//         entity.Property(x => x.EventId);
//         entity.Property(x => x.State);
//     }
// }

