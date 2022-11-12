using System.Reflection;
using Framework.Commands.CommandHandlers;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Framework.Commands.MassTransitDefaultConfig;

public static class ServiceExtension
{
    public static void MassTransitExtensions<TContext>(this IServiceCollection services,IConfiguration configuration, string projectName) where TContext : DbContext
    {
        services.TryAddSingleton(KebabCaseEndpointNameFormatter.Instance);
        services.AddMassTransit(cfg =>
        {
            cfg.AddEntityFrameworkOutbox<TContext>(o =>
            {
                o.UseSqlServer();
                o.UseBusOutbox();
            });
            var assemblies = AppDomain.CurrentDomain.GetAssemblies().Where(x => x.FullName != null && x.FullName.Contains(projectName)).ToArray();
            cfg.AddConsumers(assemblies);
            cfg.AddSagaStateMachines(assemblies);
            cfg.SetEntityFrameworkSagaRepositoryProvider(configurator =>
            {
                configurator.ConcurrencyMode = ConcurrencyMode.Optimistic;
                configurator.AddDbContext<DbContext, TContext>((provider, builder) =>
                {
                    builder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), m =>
                    {
                        m.MigrationsAssembly(Assembly.GetExecutingAssembly().GetName().Name);
                        m.MigrationsHistoryTable($"__{nameof(TContext)}");
                    });
                });

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