using System.Collections;
using System.Linq.Expressions;
using System.Reflection;
using Confluent.Kafka;
using MassTransit;
using MassTransit.Configuration;

namespace Framework.Commands.MassTransitDefaultConfig;

public static class KafkaTopicEndPointExtensions
{
    public static void AddTopicEndPoints(this IKafkaFactoryConfigurator configurator,
        IRiderRegistrationContext riderRegistrationContext, IEnumerable<Assembly> assemblies)
    {
        var allTypes = assemblies.GetAllProducers();
        foreach (var assembly in allTypes)
        {
            var myClassType = typeof(IKafkaTopicReceiveEndpointConfigurator<,>);
            var constructed = myClassType.MakeGenericType(typeof(Ignore), assembly.BaseType.GetGenericArguments()[0]);
            var ctrArgs = typeof(Action<>).MakeGenericType(constructed);
            var instance = Activator.CreateInstance(assembly,args:riderRegistrationContext);

            var myType = instance.GetType();
            var groupName = myType.GetProperty("GroupId").GetValue(instance);
            var topicName = myType.GetProperty("TopicName").GetValue(instance);
            var actionMethod = assembly.GetMethod("ActionMethod");
            var @delegate = Delegate.CreateDelegate(ctrArgs, instance, actionMethod);
            typeof(KafkaTopicEndPointExtensions).GetMethod(nameof(KafkaExtension))
                ?.MakeGenericMethod(assembly.BaseType.GetGenericArguments()[0])
                .Invoke(assembly, new object[]
                {
                    configurator,
                    topicName,
                    groupName,
                    @delegate
                });
        }
    }

    private static IEnumerable<Type> GetAllProducers(this IEnumerable<Assembly> assemblies)
    {
        // get all classes that implements "TopicEndPoint" interface from loadable assemblies
        return assemblies
            .SelectMany(assemblies => assemblies.GetTypes())
            .Where(t => t.BaseType.IsGenericType && t.BaseType.GetGenericTypeDefinition() == typeof(TopicEndPoint<>))
            .AsEnumerable();
        ;
    }

    public static void KafkaExtension<TProducer>(this IKafkaFactoryConfigurator configurator, string topicName,
        string groupId,
        Action<IKafkaTopicReceiveEndpointConfigurator<Ignore, TProducer>> configure) where TProducer : class, IProducer
    {
        configurator.TopicEndpoint<TProducer>(topicName, groupId, configure);
    }
}