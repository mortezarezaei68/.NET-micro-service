using DotNetCore.CAP.Dashboard.NodeDiscovery;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Framework.Buses;

public static class CapServiceKafkaExtensions
{
    public static void AddCapConfigureServices<TContext>(this IServiceCollection services,IConfiguration configuration) where TContext:DbContext
    {
        //......

        

        services.AddCap(x =>
        {
            // If you are using EF, you need to add the configuration：
            x.UseEntityFramework<TContext>(); //Options, Notice: You don't need to config x.UseSqlServer(""") again! CAP can autodiscovery.

            // If you are using ADO.NET, choose to add configuration you needed：
            x.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));

            // If you are using MongoDB, you need to add the configuration：

            // CAP support RabbitMQ,Kafka,AzureService as the MQ, choose to add configuration you needed：
            x.UseKafka(configuration.GetConnectionString("Kafka"));
            x.UseDashboard();

            // // Register to Consul
            // x.UseDiscovery(d =>
            // {
            //     d.DiscoveryServerHostName = "localhost";
            //     d.DiscoveryServerPort = 8500;
            //     d.CurrentNodeHostName = "localhost";
            //     d.CurrentNodePort = 5800;
            //     d.NodeId = 1;
            //     d.NodeName = "CAP No.1 Node";
            // });
        });
    }   
    public static void AddCapConfigureServices(this IServiceCollection services,IConfiguration configuration) 
    {
        //......
        services.AddCap(x =>
        {
            
            // If you are using MongoDB, you need to add the configuration：

            // CAP support RabbitMQ,Kafka,AzureService as the MQ, choose to add configuration you needed：
            x.UseKafka(configuration.GetConnectionString("Kafka"));
            x.UseDashboard();

            // // Register to Consul
            // x.UseDiscovery(d =>
            // {
            //     d.DiscoveryServerHostName = "localhost";
            //     d.DiscoveryServerPort = 8500;
            //     d.CurrentNodeHostName = "localhost";
            //     d.CurrentNodePort = 5800;
            //     d.NodeId = 1;
            //     d.NodeName = "CAP No.1 Node";
            // });
        });
    }
}
