using System.Collections;
using System.Reflection;
using System.Text.RegularExpressions;
using MassTransit;

namespace Framework.Commands.MassTransitDefaultConfig;

public static class RiderProducerExtensions
{

    public static void AddProducers(this IEnumerable riderConfiguration, Assembly[] assemblies)
    {
        var allTypes = assemblies.GetAllProducers();
        foreach (var assembly in allTypes)
        {
            typeof(RiderProducerExtensions).GetMethod(nameof(RiderExtension))
                    ?.MakeGenericMethod(assembly)
                    .Invoke(assembly, new object[]
                    {
                        riderConfiguration,
                        assembly.Name?.Underscore()
                    });
        }
    }
    
    public static void RiderExtension<TProducer>(this IRiderRegistrationConfigurator configurator,string topicName) where TProducer : class,IProducer
    {
        configurator.AddProducer<TProducer>(topicName);
    }
    private static IEnumerable<Type> GetAllProducers(this IEnumerable<Assembly> assemblies)
    {
        // get all classes that implements "IProducer" interface from loadable assemblies
        return assemblies
            .SelectMany(assemblies=>assemblies.GetTypes()).Where(type =>
                type.IsClass &&
                type.IsAssignableTo(typeof(IProducer))).AsEnumerable();
    }
    /// <summary>
    /// Separates the input words with underscore
    /// </summary>
    /// <param name="input">The string to be underscored</param>
    /// <returns></returns>
    public static string? Underscore(this string input)
    {
        return Regex.Replace(
            Regex.Replace(
                Regex.Replace(input, @"([\p{Lu}]+)([\p{Lu}][\p{Ll}])", "$1_$2"), @"([\p{Ll}\d])([\p{Lu}])", "$1_$2"), @"[-\s]", "_").ToLower();
    }

}