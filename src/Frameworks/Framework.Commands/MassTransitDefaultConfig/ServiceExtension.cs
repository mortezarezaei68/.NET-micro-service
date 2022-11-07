using System.Reflection;
using Framework.Commands.CommandHandlers;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Framework.Commands.MassTransitDefaultConfig;

public static class ServiceExtension
{
    public static void MassTransitExtensions<TContext>(this IServiceCollection services) where TContext : DbContext
    {
        services.AddMassTransit(cfg =>
        {
            cfg.AddEntityFrameworkOutbox<TContext>(o =>
            {
                // configure which database lock provider to use (Postgres, SqlServer, or MySql)
                o.UsePostgres();
                o.IsolationLevel = System.Data.IsolationLevel.ReadCommitted;
                // enable the bus outbox
                o.UseBusOutbox();
            });
            cfg.AddConsumers(type =>
            {
                var data = type.BaseType?.Name.Contains(
                    nameof(MassTransitTransactionalCommandHandler<RequestCommand, ResponseCommand>)) ?? false;
                return data;
            }, typeof(RequestCommand).Assembly);

            cfg.AddSagaStateMachine<OrderStateMachine, OrderState, RegistrationStateDefinition>()
                // .EntityFrameworkRepository(r =>
                // {
                //     r.ExistingDbContext<OrderManagementContext>();
                //     r.UsePostgres();
                // });
                .EntityFrameworkRepository(r =>
                {
                    r.ExistingDbContext<OrderManagementContext>();
                    r.UseSqlServer();
                });
            cfg.UsingInMemory((context, cfg) =>
            {
                cfg.AutoStart = true;
                cfg.ConfigureEndpoints(context);
                cfg.ConnectConsumerConfigurationObserver(new UnitOfWorkConsumerConfigurationObserver());
            });
        });
        services.AddHostedService<MassTransitConsoleHostedService>();
    }
}