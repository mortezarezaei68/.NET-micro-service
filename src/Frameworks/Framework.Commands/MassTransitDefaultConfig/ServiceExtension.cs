using System.ComponentModel;
using System.Reflection;
using Confluent.Kafka;
using Framework.Commands.CommandHandlers;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Framework.Commands.MassTransitDefaultConfig;

public static class ServiceExtension
{

    public static void MassTransitExtensions<TContext>(this IServiceCollection services,
        IConfiguration configuration, string projectName, IEnumerable<KafkaConfiguration>? kafkaSetting = null) where TContext : DbContext
    {
        services.TryAddSingleton(KebabCaseEndpointNameFormatter.Instance);
        services.AddMassTransit(cfg =>
        {
            cfg.AddEntityFrameworkOutbox<TContext>(o =>
            {
                o.UseSqlServer();
                o.UseBusOutbox();
            });
            //cfg.UsingRabbitMq((context, cfg) => cfg.ConfigureEndpoints(context));
            cfg.UsingInMemory((context, cfg) =>
            {
                cfg.AutoStart = true;
                cfg.ConfigureEndpoints(context);
                cfg.ConnectConsumerConfigurationObserver(new UnitOfWorkConsumerConfigurationObserver());
            });
            
            var assemblies = AppDomain.CurrentDomain.GetAssemblies()
                .Where(x => x.FullName != null && x.FullName.Contains(projectName)).ToArray();
      
            cfg.AddConsumers(assemblies);
            cfg.AddSagaStateMachines(assemblies);
            cfg.SetEntityFrameworkSagaRepositoryProvider(repositoryConfigurator =>
            {
                repositoryConfigurator.ConcurrencyMode = ConcurrencyMode.Optimistic;
                repositoryConfigurator.AddDbContext<DbContext, TContext>((provider, builder) =>
                {
                    builder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), m =>
                    {
                        m.MigrationsAssembly(Assembly.GetExecutingAssembly().GetName().Name);
                        m.MigrationsHistoryTable($"__{nameof(TContext)}");
                    });
                });
            });
            
            cfg.AddRider(riderConfiguration =>
            {
                riderConfiguration.AddProducers(assemblies);
                
                riderConfiguration.UsingKafka((context, kafkaConfiguration) =>
                {
                    kafkaConfiguration.Host(kafkaSetting.Select(a=>a.HostName).ToList());
                    kafkaConfiguration.AddTopicEndPoints(context,assemblies);
                });
            });
   
       
         
        });
        services.AddHostedService<MassTransitConsoleHostedService>();
    }
    

 
}
public class KafkaConfiguration 
{
    public string HostName { get; set; }        //Added set property
} 
